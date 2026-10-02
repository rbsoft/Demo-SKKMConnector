using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportSettlementAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Асинхронный отчёт о расчётах";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostReportSettlementAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.ReportSettlementAsync();
        if (connector.Ok)
        {
            Console.WriteLine($"Отчёт о расчётах поставлен в очередь. docId={connector.DocumentId}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
