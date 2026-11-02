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

        // Коды маркировки для проверки, можно указать несколько через запятую:
        connector.Codes = codes;

        // Проверяем КМ через API Честного знака:
        if (await connector.VerifyMarking())
            RequestKM.PrintVerify(connector);
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
