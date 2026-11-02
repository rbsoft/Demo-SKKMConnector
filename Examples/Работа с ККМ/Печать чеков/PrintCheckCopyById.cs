using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopyById : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия чека по идентификатору";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostPrintCheckCopyById()
    {
        connector.DeviceName = deviceName;
        connector.DocId = documentId;
        if (await connector.PrintCheckCopy())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
