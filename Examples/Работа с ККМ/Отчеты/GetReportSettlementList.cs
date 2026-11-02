using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportSettlementList : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Список отчётов расчётов";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetReportSettlementListByPeriod()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период отбора:
        connector.FromDate = fromDate;
        connector.ToDate = toDate;

        // Получаем список отчётов:
        if (await connector.GetReportSettlementList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
