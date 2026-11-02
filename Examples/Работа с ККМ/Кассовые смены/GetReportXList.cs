using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportXList : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Список X-отчётов";
    public const int SortOrder = 11;

    public async Task<SkkmConnector> GetReportXListByPeriod()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период отбора:
        connector.FromDate = fromDate;
        connector.ToDate = toDate;

        // Получаем список X-отчётов:
        if (await connector.GetReportXList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
