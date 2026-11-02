using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetSlipList : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Список нефискальных документов";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetGetSlipList()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Получаем список слип-чеков:
        if (await connector.GetSlipList())
        {
            foreach (var report in connector.ReportValues)
                Console.WriteLine(report.Key + ": " + report.Value);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
