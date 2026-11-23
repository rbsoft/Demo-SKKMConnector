using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;

namespace SkkmNugetSample.Ui;

// Подсветка C# и JSON в цветах Visual Studio (тёмная тема).
// Соседний текст одного цвета склеивается в один Run — иначе большой файл
// (напр. пример со 100+ позициями) породил бы десятки тысяч inline-объектов.
public static class CSharpHighlighter
{
    private static readonly HashSet<string> Keywords = new()
    {
        "using", "namespace", "public", "private", "protected", "internal", "static",
        "class", "struct", "enum", "interface", "async", "await", "var", "new",
        "return", "if", "else", "for", "foreach", "while", "throw", "try", "catch",
        "finally", "true", "false", "null", "this", "base", "const", "readonly",
        "void", "int", "long", "bool", "string", "decimal", "double", "float",
        "object", "byte", "short", "uint", "ulong", "get", "set", "out", "ref",
        "in", "is", "as", "typeof", "nameof", "override", "virtual", "abstract",
        "sealed", "partial", "where", "from", "select", "into", "join", "let"
    };

    private static readonly HashSet<string> Types = new()
    {
        "SkkmConnector", "Task", "InvalidOperationException", "Exception",
        "FiscalLine", "TextLine", "BarcodeLine", "SeparatorLine", "PictureLine",
        "PrintLine", "TemplateItem", "PrintFormBarcode", "Picture", "Payments", "Vendor", "Agent", "Marking", "Industry", "UserAttribute",
        "OperationalAttribute", "ElectronicPayment", "FractionalQuantity",
        "CorrectionData", "Correction105Taxes", "CorrectionTypes", "LineStyle", "CheckType", "TaxSystem"
    };

    private static readonly IBrush Default = new SolidColorBrush(Color.Parse("#DCDCDC"));
    private static readonly IBrush Keyword = new SolidColorBrush(Color.Parse("#569CD6"));
    private static readonly IBrush Type = new SolidColorBrush(Color.Parse("#4EC9B0"));
    private static readonly IBrush Str = new SolidColorBrush(Color.Parse("#CE9178"));
    private static readonly IBrush Comment = new SolidColorBrush(Color.Parse("#6A9955"));
    private static readonly IBrush Number = new SolidColorBrush(Color.Parse("#B5CEA8"));
    private static readonly IBrush Key = new SolidColorBrush(Color.Parse("#9CDCFE"));

    // <summary> из XML: типы; члены по ключу «Тип.Имя»; UniqueMembers — если имя однозначное.
    private static readonly Dictionary<string, string> TypeDocs = new(StringComparer.Ordinal);
    private static readonly Dictionary<string, string> MemberDocs = new(StringComparer.Ordinal);
    private static readonly Dictionary<string, string> UniqueMembers = new(StringComparer.Ordinal);
    private static readonly HashSet<string> TypeNames = new(StringComparer.Ordinal);
    private static bool _docsLoaded;

    private static void EnsureDocs()
    {
        if (_docsLoaded)
            return;
        _docsLoaded = true;

        var firstByShort = new Dictionary<string, string>(StringComparer.Ordinal);
        var ambiguous = new HashSet<string>(StringComparer.Ordinal);
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SkkmConnector.xml");
            if (File.Exists(path))
            {
                foreach (var member in XDocument.Load(path).Descendants("member"))
                {
                    var name = (string?)member.Attribute("name");
                    var summary = member.Element("summary");
                    if (name is null || summary is null || name.Length < 2)
                        continue;

                    char kind = name[0];
                    if (kind != 'T' && kind != 'P' && kind != 'F' && kind != 'M')
                        continue;
                    if (kind == 'M' && name.Contains("#ctor", StringComparison.Ordinal))
                        continue;

                    var doc = BuildDoc(summary);
                    if (doc.Length == 0)
                        continue;

                    var shortName = ShortName(name);
                    if (kind == 'T')
                    {
                        TypeDocs[shortName] = doc;
                        TypeNames.Add(shortName);
                        continue;
                    }

                    var qualified = QualifiedName(name);
                    if (qualified is null || MemberDocs.ContainsKey(qualified))
                        continue;
                    MemberDocs[qualified] = doc;

                    if (firstByShort.TryGetValue(shortName, out var prev))
                    {
                        if (prev != doc)
                            ambiguous.Add(shortName);
                    }
                    else
                    {
                        firstByShort[shortName] = doc;
                    }
                }
            }
        }
        catch
        {
            // нет файла или битый XML — просто без подсказок
        }

        foreach (var kv in firstByShort)
        {
            if (!ambiguous.Contains(kv.Key))
                UniqueMembers[kv.Key] = kv.Value;
        }
    }

    /// <summary>Возвращает summary типа/члена под символом code[index] или null.</summary>
    public static string? SummaryAt(string code, int index)
    {
        EnsureDocs();
        if (string.IsNullOrEmpty(code) || index < 0 || index >= code.Length)
            return null;

        if (!IsWordChar(code[index]))
        {
            if (index > 0 && IsWordChar(code[index - 1])) index--;
            else return null;
        }

        int s = index, e = index;
        while (s > 0 && IsWordChar(code[s - 1])) s--;
        while (e + 1 < code.Length && IsWordChar(code[e + 1])) e++;
        if (!(char.IsLetter(code[s]) || code[s] == '_'))
            return null;

        string word = code.Substring(s, e - s + 1);

        int p = s - 1;
        while (p >= 0 && char.IsWhiteSpace(code[p])) p--;
        bool afterDot = p >= 0 && code[p] == '.';

        string? type = afterDot ? TypeBeforeDot(code, p) : EnclosingNewType(code, s);
        if (type != null && MemberDocs.TryGetValue(type + "." + word, out var typed))
            return typed;
        if (!afterDot && TypeDocs.TryGetValue(word, out var tdoc))
            return tdoc;
        if (UniqueMembers.TryGetValue(word, out var unique))
            return unique;
        if (afterDot && MemberDocs.TryGetValue("SkkmConnector." + word, out var kkm))
            return kkm;
        return afterDot ? TypeDocs.GetValueOrDefault(word) : null;
    }

    private static bool IsWordChar(char c) => char.IsLetterOrDigit(c) || c == '_';

    private static string StripDocId(string docName)
    {
        var s = docName.Length > 1 && docName[1] == ':' ? docName[2..] : docName;
        int paren = s.IndexOf('(');
        return paren >= 0 ? s[..paren] : s;
    }

    private static string ShortName(string docName)
    {
        var s = StripDocId(docName);
        int dot = s.LastIndexOf('.');
        return dot >= 0 ? s[(dot + 1)..] : s;
    }

    private static string? QualifiedName(string docName)
    {
        var s = StripDocId(docName);
        int dot = s.LastIndexOf('.');
        if (dot < 0)
            return null;
        int prev = s.LastIndexOf('.', dot - 1);
        string type = prev >= 0 ? s[(prev + 1)..dot] : s[..dot];
        return type + "." + s[(dot + 1)..];
    }

    private static string? TypeBeforeDot(string code, int dot)
    {
        int e = dot - 1;
        while (e >= 0 && char.IsWhiteSpace(code[e])) e--;
        int s = e;
        while (s >= 0 && IsWordChar(code[s])) s--;
        if (e < s + 1)
            return null;
        string left = code.Substring(s + 1, e - s);
        return TypeNames.Contains(left) ? left : null;
    }

    // Тип из ближайшего `new TypeName {` — для свойств в object initializer.
    private static string? EnclosingNewType(string code, int from)
    {
        int depth = 0;
        for (int i = from - 1; i >= 0; i--)
        {
            char c = code[i];
            if (c is '"' or '\'')
            {
                char q = c;
                i--;
                while (i >= 0 && code[i] != q)
                    i--;
                continue;
            }
            if (c == '}')
                depth++;
            else if (c == '{')
            {
                if (depth > 0)
                {
                    depth--;
                    continue;
                }
                return TypeBeforeInitializer(code, i);
            }
        }
        return null;
    }

    private static string? TypeBeforeInitializer(string code, int brace)
    {
        int p = brace - 1;
        while (p >= 0 && char.IsWhiteSpace(code[p])) p--;
        if (p >= 0 && code[p] == ')')
        {
            int paren = 1;
            p--;
            while (p >= 0 && paren > 0)
            {
                if (code[p] == ')') paren++;
                else if (code[p] == '(') paren--;
                p--;
            }
            while (p >= 0 && char.IsWhiteSpace(code[p])) p--;
        }
        if (p >= 0 && code[p] == '>')
        {
            int angle = 1;
            p--;
            while (p >= 0 && angle > 0)
            {
                if (code[p] == '>') angle++;
                else if (code[p] == '<') angle--;
                p--;
            }
            while (p >= 0 && char.IsWhiteSpace(code[p])) p--;
        }
        int e = p;
        while (p >= 0 && IsWordChar(code[p])) p--;
        if (e < p + 1)
            return null;
        string type = code.Substring(p + 1, e - p);
        while (p >= 0 && char.IsWhiteSpace(code[p])) p--;
        int ks = p;
        while (ks >= 0 && IsWordChar(code[ks])) ks--;
        if (ks + 1 > p)
            return null;
        return code.Substring(ks + 1, p - ks) == "new" ? type : null;
    }

    private static string BuildDoc(XElement el)
    {
        var sb = new StringBuilder();
        foreach (var node in el.Nodes())
            RenderDoc(node, sb);

        var lines = new List<string>();
        foreach (var raw in sb.ToString().Split('\n'))
        {
            var line = Regex.Replace(raw, @"[ \t]+", " ").Trim();
            if (line.Length > 0)
                lines.Add(line);
        }
        return string.Join("\n", lines);
    }

    private static void RenderDoc(XNode node, StringBuilder sb)
    {
        switch (node)
        {
            case XText t:
                sb.Append(t.Value);
                break;
            case XElement e:
                switch (e.Name.LocalName)
                {
                    case "para":
                        sb.Append('\n');
                        foreach (var c in e.Nodes()) RenderDoc(c, sb);
                        sb.Append('\n');
                        break;
                    case "br":
                        sb.Append('\n');
                        break;
                    case "see":
                    case "seealso":
                        var cref = (string?)e.Attribute("cref");
                        if (!string.IsNullOrEmpty(cref)) sb.Append(ShortName(cref));
                        else foreach (var c in e.Nodes()) RenderDoc(c, sb);
                        break;
                    default:
                        foreach (var c in e.Nodes()) RenderDoc(c, sb);
                        break;
                }
                break;
        }
    }

    public static void Apply(string code, InlineCollection target)
    {
        target.Clear();
        EnsureDocs();

        var buffer = new StringBuilder();
        var brush = Default;

        void Emit(int start, int end, IBrush color)
        {
            if (!ReferenceEquals(color, brush) && buffer.Length > 0)
            {
                target.Add(new Run(buffer.ToString()) { Foreground = brush });
                buffer.Clear();
            }
            brush = color;
            buffer.Append(code, start, end - start);
        }

        int i = 0, n = code.Length;
        string prev = "";
        bool afterDot = false;
        while (i < n)
        {
            char c = code[i];
            int start = i;

            if (c == '/' && i + 1 < n && code[i + 1] == '/')          // // комментарий
            {
                while (i < n && code[i] != '\n') i++;
                Emit(start, i, Comment); prev = ""; afterDot = false;
            }
            else if (c == '/' && i + 1 < n && code[i + 1] == '*')     // /* комментарий */
            {
                i += 2;
                while (i < n && !(code[i] == '*' && i + 1 < n && code[i + 1] == '/')) i++;
                if (i < n) i += 2;
                Emit(start, i, Comment); prev = ""; afterDot = false;
            }
            else if (c == '"' || (c == '@' && i + 1 < n && code[i + 1] == '"') || c == '\'')  // строка / символ
            {
                i = SkipQuoted(code, i, n);
                Emit(start, i, Str); prev = ""; afterDot = false;
            }
            else if (char.IsLetter(c) || c == '_')                    // слово (ключевое / тип / прочее)
            {
                while (i < n && (char.IsLetterOrDigit(code[i]) || code[i] == '_')) i++;
                string word = code.Substring(start, i - start);
                bool isType = !afterDot && (Types.Contains(word) || prev == "new" || TypeNames.Contains(word));
                var color = Keywords.Contains(word) ? Keyword
                          : isType ? Type
                          : Default;
                Emit(start, i, color);
                prev = word;
                afterDot = false;
            }
            else if (char.IsDigit(c))                                 // число
            {
                while (i < n && (char.IsLetterOrDigit(code[i]) || code[i] == '.' || code[i] == '_')) i++;
                Emit(start, i, Number); prev = ""; afterDot = false;
            }
            else                                                      // пробелы, скобки, операторы
            {
                i++;
                Emit(start, i, Default);
                if (!char.IsWhiteSpace(c)) { prev = ""; afterDot = c == '.'; }
            }
        }

        if (buffer.Length > 0)
            target.Add(new Run(buffer.ToString()) { Foreground = brush });
    }

    // Вывод примера: строки «Ключ: значение».
    public static void ApplyResponse(string text, InlineCollection target)
    {
        target.Clear();
        foreach (var line in Regex.Split(text, "(?<=\n)"))
        {
            int colon = line.IndexOf(": ", StringComparison.Ordinal) is var i and >= 0 ? i + 2 : 0;
            var value = line[colon..].TrimEnd();
            target.Add(new Run(line[..colon]) { Foreground = Key });
            target.Add(new Run(line[colon..])
            {
                Foreground = value is "True" or "False" ? Keyword
                    : decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out _) ? Number
                    : Str
            });
        }
    }

    // Пропускает строку "...", дословную @"...""..." или символ '\''; возвращает индекс за закрывающей кавычкой.
    private static int SkipQuoted(string code, int i, int n)
    {
        bool verbatim = code[i] == '@';
        char quote = verbatim ? '"' : code[i];
        i += verbatim ? 2 : 1;
        while (i < n)
        {
            if (verbatim)
            {
                if (code[i] == '"' && i + 1 < n && code[i + 1] == '"') { i += 2; continue; }
            }
            else if (code[i] == '\\' && i + 1 < n) { i += 2; continue; }

            if (code[i] == quote) { i++; break; }
            i++;
        }
        return i;
    }
}
