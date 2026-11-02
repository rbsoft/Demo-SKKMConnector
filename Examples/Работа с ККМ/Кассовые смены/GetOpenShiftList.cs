using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOpenShiftList : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Список открытий смен";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetOpenShiftListByPeriod()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период отбора:
        connector.FromDate = fromDate;
        connector.ToDate = toDate;

        // Получаем список открытий смен:
        if (await connector.GetOpenShiftList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
