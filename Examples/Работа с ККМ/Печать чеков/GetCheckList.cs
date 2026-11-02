using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckList : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Список чеков за смену";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> GetGetCheckList()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период отбора:
        connector.FromDate = fromDate;
        connector.ToDate = toDate;

        // Номер смены:
        connector.ShiftNumber = shiftNumber;

        // Получаем список чеков смены:
        if (await connector.GetCheckList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
