using System.IO;
using System.Text.RegularExpressions;

namespace SkkmNugetSample.Ui;

public static class ExampleSource
{
    // Индекс файлов примеров строится один раз.
    private static readonly Lazy<IReadOnlyDictionary<string, string>> FileIndex = new(BuildFileIndex);

    public static string Read(ExampleItem example)
    {
        var raw = RawText(example.Instance.GetType().Name);
        return raw == null ? "" : FormatForDisplay(raw);
    }

    public static string? RawText(string className)
        => FileIndex.Value.TryGetValue(className + ".cs", out var path) ? File.ReadAllText(path) : null;

    /// <summary>
    /// Имя вызываемого метода коннектора (последний <c>await connector.X(</c>) — для дерева и заголовка.
    /// </summary>
    public static string ConnectorMethod(string className, string fallback)
    {
        var raw = RawText(className);
        if (raw == null)
            return fallback;
        var matches = Regex.Matches(raw, @"await (?:kkm|connector)\.([A-Za-z0-9]+)\(");
        return matches.Count > 0 ? matches[^1].Groups[1].Value : fallback;
    }

    private static IReadOnlyDictionary<string, string> BuildFileIndex()
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var root in Roots())
        {
            var examplesDir = Path.Combine(root, "Examples");
            if (!Directory.Exists(examplesDir))
                continue;

            foreach (var file in Directory.EnumerateFiles(examplesDir, "*.cs", SearchOption.AllDirectories))
                map.TryAdd(Path.GetFileName(file), file);
        }
        return map;
    }

    // Тело метода примера + создание коннектора в начале, как в клиентском коде.
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
            if (text[i] == '{') depth++;
            else if (text[i] == '}')
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

        var body = Unindent(text[(brace + 1)..end].TrimEnd());
        body = Regex.Replace(body, @"\r?\nreturn connector;\s*$", "", RegexOptions.Multiline).TrimEnd();
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
