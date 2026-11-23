using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportSettlementAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Асинхронный отчёт о расчётах";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostReportSettlementAsync()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        if (await connector.ReportSettlementAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}