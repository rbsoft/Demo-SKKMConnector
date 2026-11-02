using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopy : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия чека по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostPrintCheckCopy()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
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