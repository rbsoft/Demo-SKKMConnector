using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCloseShiftList : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Список закрытий смен";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> GetCloseShiftListByPeriod()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период отбора:
        connector.FromDate = fromDate;
        connector.ToDate = toDate;

        // Получаем список закрытий смен:
        if (await connector.GetCloseShiftList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}