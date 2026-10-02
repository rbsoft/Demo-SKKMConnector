using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetVersion : Sample
{
    public const string GroupPath = "Служебные";
    public const string Title = "Текущая версия сервера";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetVersion()
    {
        await connector.GetVersion();
        if (connector.Ok)
            Console.WriteLine($"Версия сервера: {connector.ServerVersion}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
