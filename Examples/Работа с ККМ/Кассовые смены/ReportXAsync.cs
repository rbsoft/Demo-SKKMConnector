using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportXAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Асинхронное получение X-отчёта";
    public const int SortOrder = 9;

    public async Task<SkkmConnector> PostReportXAsync()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        if (await connector.ReportXAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
