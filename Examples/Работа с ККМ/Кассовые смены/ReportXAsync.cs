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
        connector.SetCashier(cashierName, cashierVatin);
        await connector.ReportXAsync();
        if (connector.Ok)
        {
            Console.WriteLine($"X-отчёт поставлен в очередь. docId={connector.DocumentId}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
