using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class VerifyMarkingLmcz : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ через ЛМ ЧЗ";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> PostVerifyMarkingLmcz()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Коды маркировки для проверки, можно указать несколько через запятую:
        connector.Codes = codes;

        // Проверяем КМ через локальный модуль:
        if (await connector.VerifyMarkingLmcz())
            RequestKM.PrintVerify(connector);
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
