using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckList : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Список чеков";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> GetGetCheckList()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период необязателен (пусто в панели — from/to не уходят):
        connector.FromDate = fromDate;
        connector.ToDate = toDate;
        // Смена и признак обработки необязательны:
        connector.ShiftNumber = shiftNumber;
        connector.ListIsProcessed = isProcessed;

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
