using System.Reflection;
using System.Text.RegularExpressions;
using RBSoftSkkm;
using SkkmNugetSample.Examples;

namespace SkkmNugetSample.Ui;

public sealed class ExampleItem
{
    public string[] GroupPath { get; init; } = [];
    public string Title { get; init; } = "";
    public string MethodName { get; init; } = "";
    public bool DocumentIdOptional { get; init; }
    public bool GuidOptional { get; init; }
    public int SortOrder { get; init; }
    public HashSet<string> Fields { get; init; } = new(StringComparer.Ordinal);
    public Sample Instance { get; init; } = null!;
    public MethodInfo Method { get; init; } = null!;

    public bool Uses(string field) => Fields.Contains(field);

    public Task<SkkmConnector> Run()
        => (Task<SkkmConnector>)Method.Invoke(Instance, null)!;
}

public sealed class ExampleTreeNode
{
    public string Title { get; init; } = "";
    public ExampleItem? Item { get; init; }
    public List<ExampleTreeNode> Children { get; } = [];
}

public static class Examples
{
    private static readonly string[] RootOrder =
        ["Авторизация", "Служебные", "Администрирование", "Работа с ККМ"];

    private static readonly Dictionary<string, int> FolderOrder = new(StringComparer.Ordinal)
    {
        ["ККТ"] = 0,
        ["Служба"] = 1,
        ["Кассовые смены"] = 0,
        ["Отчеты"] = 1,
        ["Печать чеков"] = 2,
        ["Примеры чеков"] = 0,
        ["Корректировочные чеки"] = 3,
        ["Корректировки ФФД 1.0.5"] = 0,
        ["Корректировки ФФД 1.2"] = 1,
        ["Денежный ящик"] = 4,
        ["Нефискальные чеки"] = 5,
        ["Очередь"] = 6,
        ["Работа с маркировкой"] = 7,
    };

    // Поля панели, которые ищем в исходнике примера.
    private static readonly string[] PanelFields =
    [
        "cashierName", "cashierVatin", "documentId", "fromDate", "toDate", "filePath",
        "pool", "checkNumber", "cashAmount", "guid", "confirmationType", "codes",
        "shiftNumber", "isProcessed", "markingCode", "markingQuantity", "measureOfQuantity",
        "fractionalQuantityNumerator", "fractionalQuantityDenominator",
        "plannedStatus", "notSendToServer", "waitForResult",
    ];

    public static IReadOnlyList<ExampleTreeNode> Tree { get; } = BuildTree();

    private static IReadOnlyList<ExampleTreeNode> BuildTree()
    {
        var roots = new Dictionary<string, ExampleTreeNode>(StringComparer.Ordinal);
        foreach (var item in Discover())
            Place(roots, item);

        return roots.Values
            .OrderBy(n => IndexOf(RootOrder, n.Title))
            .Select(SortNode)
            .ToArray();
    }

    private static IEnumerable<ExampleItem> Discover()
        => typeof(Sample).Assembly.GetTypes()
            .Where(t => t.IsClass && t.IsSubclassOf(typeof(Sample)))
            .Select(From);

    private static void Place(Dictionary<string, ExampleTreeNode> roots, ExampleItem item)
    {
        var path = item.GroupPath.Length == 0 ? ["Прочее"] : item.GroupPath;
        var node = Folder(roots, path[0]);
        for (var i = 1; i < path.Length; i++)
            node = Folder(node, path[i]);
        node.Children.Add(new ExampleTreeNode { Title = item.Title, Item = item });
    }

    private static ExampleTreeNode Folder(Dictionary<string, ExampleTreeNode> roots, string title)
    {
        if (!roots.TryGetValue(title, out var node))
            roots[title] = node = new ExampleTreeNode { Title = title };
        return node;
    }

    private static ExampleTreeNode Folder(ExampleTreeNode parent, string title)
    {
        var node = parent.Children.FirstOrDefault(c => c.Item == null && c.Title == title);
        if (node != null)
            return node;
        node = new ExampleTreeNode { Title = title };
        parent.Children.Add(node);
        return node;
    }

    private static ExampleTreeNode SortNode(ExampleTreeNode node)
    {
        node.Children.Sort((a, b) =>
        {
            if (a.Item == null && b.Item == null)
                return FolderOrder.GetValueOrDefault(a.Title, int.MaxValue)
                    .CompareTo(FolderOrder.GetValueOrDefault(b.Title, int.MaxValue));
            if (a.Item == null) return -1;
            if (b.Item == null) return 1;
            var byOrder = a.Item.SortOrder.CompareTo(b.Item.SortOrder);
            return byOrder != 0 ? byOrder : string.Compare(a.Title, b.Title, StringComparison.Ordinal);
        });

        foreach (var child in node.Children.Where(c => c.Item == null))
            SortNode(child);
        return node;
    }

    private static ExampleItem From(Type type)
    {
        var method = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Single(m => m.ReturnType == typeof(Task<SkkmConnector>));

        var source = ExampleSource.RawText(type.Name) ?? "";
        bool Mentions(string field) => Regex.IsMatch(source, $@"(?<![.\w]){field}\b");

        var fields = PanelFields.Where(Mentions).ToHashSet(StringComparer.Ordinal);
        // RequestKM не пишет guid в коде, но поле нужно для ConfirmKM после ответа.
        if (Const(type, "NeedGuid") is true)
            fields.Add("guid");

        var group = Const(type, "GroupPath") as string ?? "Прочее";

        return new ExampleItem
        {
            GroupPath = group.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            Title = Const(type, "Title") as string ?? type.Name,
            MethodName = ExampleSource.ConnectorMethod(type.Name, type.Name),
            DocumentIdOptional = Const(type, "DocumentIdOptional") is true,
            GuidOptional = Const(type, "GuidOptional") is true,
            SortOrder = Const(type, "SortOrder") is int order ? order : int.MaxValue,
            Fields = fields,
            Instance = (Sample)Activator.CreateInstance(type)!,
            Method = method,
        };
    }

    private static object? Const(Type type, string name)
        => type.GetField(name, BindingFlags.Public | BindingFlags.Static)?.GetRawConstantValue();

    private static int IndexOf(string[] order, string title)
    {
        var index = Array.IndexOf(order, title);
        return index < 0 ? int.MaxValue : index;
    }
}
