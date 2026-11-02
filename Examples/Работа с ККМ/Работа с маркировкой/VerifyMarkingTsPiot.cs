using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class VerifyMarkingTsPiot : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ через ТС ПИоТ";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> PostVerifyMarkingTsPiot()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Коды маркировки для проверки, можно указать несколько через запятую:
        connector.Codes = codes;

        // Проверяем КМ через ТС ПИоТ:
        if (await connector.VerifyMarkingTsPiot())
            RequestKM.PrintVerify(connector);
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
