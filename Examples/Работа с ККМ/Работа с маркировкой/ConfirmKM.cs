using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ConfirmKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Подтвердить КМ";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> PostConfirmKM()
    {
        connector.DeviceName = deviceName;
        connector.RequestKmGuid = "guid-из-проверки-КМ";
        connector.ConfirmationType = KmConfirmationType.Included;
        await connector.ConfirmKM();

        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}