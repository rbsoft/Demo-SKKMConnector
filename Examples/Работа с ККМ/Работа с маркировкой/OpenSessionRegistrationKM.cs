using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OpenSessionRegistrationKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Открыть сессию КМ";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostOpenSessionRegistrationKM()
    {
        connector.DeviceName = deviceName;
        await connector.OpenSessionRegistrationKM();

        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}