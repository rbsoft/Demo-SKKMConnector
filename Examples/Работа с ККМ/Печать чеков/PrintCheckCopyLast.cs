using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopyLast : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия последнего чека";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostPrintCheckCopyLast()
    {
        connector.DeviceName = deviceName;
        if (await connector.PrintCheckCopyLast())
            Console.WriteLine(
                "Копия последнего чека напечатана.\n" +
                $"Идентификатор документа (docId): {connector.DocId}");
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
