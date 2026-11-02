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
        // Guid из ответа проверки КМ (RequestKM):
        connector.Guid = guid;
        // Включён ли код маркировки в документ реализации:
        connector.ConfirmationType = confirmationType;
        if (await connector.ConfirmKM())
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}