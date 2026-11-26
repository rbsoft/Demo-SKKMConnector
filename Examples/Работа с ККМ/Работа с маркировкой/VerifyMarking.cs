using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class VerifyMarking : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> PostVerifyMarking()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Список КМ (Cis/DataMatrix), несколько КМ перечисляем через запятую:
        connector.Codes = codes;
        if (await connector.VerifyMarking())
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
