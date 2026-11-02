using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCorrection105List : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Список чеков коррекции 1.0.5";

    public async Task<SkkmConnector> GetCheckCorrection105ListItems()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Получаем список чеков коррекции:
        if (await connector.GetCorrection105List())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
