using System.IO;
using System.Text.RegularExpressions;

namespace SkkmNugetSample.Ui;

public static class ExampleSource
{
    public static string Read(ExampleItem example)
    {
        var raw = RawText(example.Instance.GetType().Name);
        return raw == null ? "" : FormatForDisplay(raw);
    }

    // Индекс файлов примеров (имя файла -> полный путь) строится один раз:
    // папка Examples сканируется единожды, а не на каждый пример.
    private static readonly Lazy<IReadOnlyDictionary<string, string>> FileIndex = new(BuildFileIndex);

    private static IReadOnlyDictionary<string, string> BuildFileIndex()
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var root in Roots())
        {
            string examplesDir = Path.Combine(root, "Examples");
            if (!Directory.Exists(examplesDir))
                continue;

            foreach (var file in Directory.EnumerateFiles(examplesDir, "*.cs", SearchOption.AllDirectories))
            {
                string name = Path.GetFileName(file);
                // Первый корень (папка вывода) имеет приоритет — как и раньше.
                map.TryAdd(name, file);
            }
        }
        return map;
    }

    /// <summary>
    /// Сырой текст файла примера по имени класса (или null, если файл не найден).
    /// </summary>
    public static string? RawText(string className)
        => FileIndex.Value.TryGetValue(className + ".cs", out var path) ? File.ReadAllText(path) : null;

    /// <summary>
    /// Имя вызываемого метода коннектора (последний <c>await kkm.X(</c>) — для показа в дереве и заголовке.
    /// </summary>
    public static string ConnectorMethod(string className, string fallback)
    {
        var raw = RawText(className);
        if (raw == null)
            return fallback;
        var matches = Regex.Matches(raw, @"await (?:kkm|connector)\.([A-Za-z0-9]+)\(");
        return matches.Count > 0 ? matches[^1].Groups[1].Value : fallback;
    }

    // Как в реальном клиентском коде: using + создание коннектора + тело запроса.
    private static string FormatForDisplay(string text)
    {
        int start = text.IndexOf("    public async Task", StringComparison.Ordinal);
        if (start < 0)
            return text.Trim();

        int brace = text.IndexOf('{', start);
        if (brace < 0)
            return text.Trim();

        int depth = 0;
        int end = -1;
        for (int i = brace; i < text.Length; i++)
        {
            char c = text[i];
            if (c == '{') depth++;
            else if (c == '}')
            {
                depth--;
                if (depth == 0)
                {
                    end = i;
                    break;
                }
            }
        }
        if (end < 0)
            return text.Trim();

        string body = Unindent(text[(brace + 1)..end].TrimEnd());
        body = Regex.Replace(body, @"\r?\nreturn connector;\s*$", "", RegexOptions.Multiline);
        body = body.TrimEnd();

        return "using RBSoftSkkm;\n\nusing var connector = new SkkmConnector();\n" + body + "\n";
    }

    private static string Unindent(string text)
    {
        var lines = text.Replace("\r\n", "\n").Split('\n');
        int pad = lines
            .Where(line => line.Trim().Length > 0)
            .Select(line => line.TakeWhile(char.IsWhiteSpace).Count())
            .DefaultIfEmpty(0)
            .Min();
        if (pad <= 0)
            return text;
        return string.Join("\n", lines.Select(line => line.Length >= pad ? line[pad..] : line));
    }

    private static IEnumerable<string> Roots()
    {
        yield return AppContext.BaseDirectory;

        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "SkkmNugetSample.csproj")))
            {
                yield return dir.FullName;
                yield break;
            }
            dir = dir.Parent;
        }
    }
}
