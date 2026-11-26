using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ConfirmKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Подтвердить КМ";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> PostConfirmKM()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // Guid запроса
        connector.Guid = guid;
        // Тип подтверждения
        connector.ConfirmationType = confirmationType;
        if (await connector.ConfirmKM())
            Console.WriteLine("КМ подтвержден");
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}