using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportSettlement : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Отчёт расчётов";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostReportSettlement()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        if (await connector.ReportSettlement())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}