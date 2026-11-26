using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCorrection120List : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Список чеков коррекции 1.2";

    public async Task<SkkmConnector> GetCheckCorrection120ListItems()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Период необязателен (пусто в панели — from/to не уходят):
        connector.FromDate = fromDate;
        connector.ToDate = toDate;
        // Смена и признак обработки необязательны:
        connector.ShiftNumber = shiftNumber;
        connector.IsProcessed = isProcessed;

        if (await connector.GetCorrection120List())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine($"{report.Key}: {report.Value} — {report.Description}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
