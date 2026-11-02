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
        if (await connector.OpenSessionRegistrationKM())
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}