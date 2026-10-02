using System.Reflection;
using RBSoftSkkm;
using SkkmNugetSample.Examples;

namespace SkkmNugetSample.Ui;

public class ExampleItem
{
    public string[] GroupPath { get; init; } = [];
    public string Title { get; init; } = "";
    public string MethodName { get; init; } = "";
    public bool NeedDocumentId { get; init; }
    public bool NeedFilePath { get; init; }
    public int SortOrder { get; init; }
    public Sample Instance { get; init; } = null!;
    public MethodInfo Method { get; init; } = null!;

    public string HttpMethod => Method.Name switch
    {
        _ when Method.Name.StartsWith("Get", StringComparison.Ordinal) => "GET",
        _ when Method.Name.StartsWith("Delete", StringComparison.Ordinal) => "DELETE",
        _ when Method.Name.StartsWith("Put", StringComparison.Ordinal) => "PUT",
        _ => "POST"
    };
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
    [
        "Авторизация",
        "Служебные",
        "Администрирование",
        "Работа с ККМ"
    ];

    private static readonly Dictionary<string, int> KnownOrder = new(StringComparer.Ordinal)
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
        ["Рекламные чеки"] = 0,
        ["Работа с картинками"] = 1,
        ["Очередь"] = 6,
        ["Работа с маркировкой"] = 7,
        ["Фискализация"] = 8,
        ["Шаблоны чека"] = 9,
        ["Операции"] = 10
    };

    public static IReadOnlyList<ExampleTreeNode> Tree { get; } = BuildTree();

    private static IReadOnlyList<ExampleTreeNode> BuildTree()
    {
        var items = typeof(Sample).Assembly.GetTypes()
            .Where(t => t.IsClass && t.IsSubclassOf(typeof(Sample)))
            .Select(From)
            .OrderBy(x => string.Join('/', x.GroupPath))
            .ThenBy(x => x.Title)
            .ToArray();

        var roots = new Dictionary<string, ExampleTreeNode>(StringComparer.Ordinal);
        foreach (var item in items)
            AddItem(roots, item);

        return roots.Values
            .OrderBy(n => RootIndex(n.Title))
            .Select(SortNode)
            .ToArray();
    }

    private static void AddItem(Dictionary<string, ExampleTreeNode> roots, ExampleItem item)
    {
        if (item.GroupPath.Length == 0)
        {
            GetOrCreateRoot(roots, "Прочее").Children.Add(new ExampleTreeNode { Title = item.Title, Item = item });
            return;
        }

        var node = GetOrCreateRoot(roots, item.GroupPath[0]);
        for (var i = 1; i < item.GroupPath.Length; i++)
            node = GetOrCreateChild(node, item.GroupPath[i]);

        node.Children.Add(new ExampleTreeNode { Title = item.Title, Item = item });
    }

    private static ExampleTreeNode GetOrCreateRoot(Dictionary<string, ExampleTreeNode> roots, string title)
    {
        if (!roots.TryGetValue(title, out var node))
        {
            node = new ExampleTreeNode { Title = title };
            roots[title] = node;
        }
        return node;
    }

    private static ExampleTreeNode GetOrCreateChild(ExampleTreeNode parent, string title)
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
                return ChildIndex(a.Title).CompareTo(ChildIndex(b.Title));
            if (a.Item == null)
                return -1;
            if (b.Item == null)
                return 1;
            var byOrder = a.Item.SortOrder.CompareTo(b.Item.SortOrder);
            if (byOrder != 0)
                return byOrder;
            return string.Compare(a.Title, b.Title, StringComparison.Ordinal);
        });

        foreach (var child in node.Children.Where(c => c.Item == null))
            SortNode(child);

        return node;
    }

    private static ExampleItem From(Type type)
    {
        var method = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Single(m => m.ReturnType == typeof(Task<SkkmConnector>));

        var groupPath = Field(type, "GroupPath") as string;
        if (string.IsNullOrWhiteSpace(groupPath))
            groupPath = Field(type, "Group") as string ?? "Прочее";

        return new ExampleItem
        {
            GroupPath = groupPath.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            Title = Field(type, "Title") as string ?? type.Name,
            MethodName = ExampleSource.ConnectorMethod(type.Name, type.Name),
            NeedDocumentId = Field(type, "NeedDocumentId") is true,
            NeedFilePath = Field(type, "NeedFilePath") is true,
            SortOrder = Field(type, "SortOrder") is int order ? order : int.MaxValue,
            Instance = (Sample)Activator.CreateInstance(type)!,
            Method = method
        };
    }

    private static object? Field(Type type, string name)
        => type.GetField(name, BindingFlags.Public | BindingFlags.Static)?.GetRawConstantValue();

    private static int RootIndex(string title)
    {
        var index = Array.IndexOf(RootOrder, title);
        return index < 0 ? int.MaxValue : index;
    }

    private static int ChildIndex(string title)
        => KnownOrder.TryGetValue(title, out var index) ? index : int.MaxValue;
}

public static class ExampleRunner
{
    public static SkkmConnector Session { get; } = new();

    public static void ApplyConnection(Sample sample, ConnectionSettings connection)
    {
        Session.Address = connection.Address.Trim();
        Session.Token = connection.Token.Trim();
        sample.connector = Session;
        sample.deviceName = connection.Device.Trim();
        sample.cashierName = connection.Cashier.Trim();
        sample.cashierVatin = connection.CashierVatin.Trim();
        sample.documentId = connection.DocumentId.Trim();
        sample.filePath = connection.FilePath.Trim();
        sample.fromDate = connection.From;
        sample.toDate = connection.To;
        Session.ShiftsFrom = connection.From;
        Session.ShiftsTo = connection.To;
    }

    public static Task<SkkmConnector> Invoke(ExampleItem example)
        => (Task<SkkmConnector>)example.Method.Invoke(example.Instance, null)!;
}
