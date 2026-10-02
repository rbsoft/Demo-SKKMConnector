using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintLastCheckCopy : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия последнего чека";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostPrintLastCheckCopy()
    {
        connector.DeviceName = deviceName;
        await connector.PrintLastCheckCopy();

        if (connector.Ok)
            Console.WriteLine(
                "Копия последнего чека напечатана.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
