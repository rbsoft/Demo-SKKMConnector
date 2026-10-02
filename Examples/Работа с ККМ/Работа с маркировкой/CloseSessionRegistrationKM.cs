using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CloseSessionRegistrationKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Закрыть сессию КМ";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostCloseSessionRegistrationKM()
    {
        connector.DeviceName = deviceName;
        await connector.CloseSessionRegistrationKM();

        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}