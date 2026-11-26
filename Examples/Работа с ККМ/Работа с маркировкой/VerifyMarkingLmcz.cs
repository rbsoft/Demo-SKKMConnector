using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class VerifyMarkingLmcz : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ ЛМ ЧЗ";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> PostVerifyMarkingLmcz()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Список КМ (Cis/DataMatrix), несколько КМ перечисляем через запятую:
        connector.Codes = codes;
        if (await connector.VerifyMarkingLmcz())
        {
            foreach (var report in connector.ReportValues)
            {
                Console.WriteLine(string.IsNullOrEmpty(report.Description)
                    ? $"{report.Key}: {report.Value}"
                    : $"{report.Key}: {report.Value} — {report.Description}");
            }
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
