using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCashInList : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Список внесений";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetGetCashInList()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период необязателен (пусто в панели — from/to не уходят):
        connector.FromDate = fromDate;
        connector.ToDate = toDate;
        // Смена и признак обработки необязательны:
        connector.ShiftNumber = shiftNumber;
        connector.IsProcessed = isProcessed;

        if (await connector.GetCashInList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine($"{report.Key}: {report.Value} — {report.Description}");


        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
