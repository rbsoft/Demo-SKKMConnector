using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Ping : Sample
{
    public const string GroupPath = "Служебные";
    public const string Title = "Проверка доступности сервера";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> GetPing()
    {
        await connector.Ping();
        if (connector.Ok)
            Console.WriteLine(
                "Сервер доступен.\n" +
                $"Продукт: {connector.ServerProduct}\n" +
                $"Версия сервера ККМ: {connector.ServerVersion}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
