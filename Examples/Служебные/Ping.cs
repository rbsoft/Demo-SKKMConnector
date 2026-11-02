using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Ping : Sample
{
    public const string GroupPath = "Служебные";
    public const string Title = "Проверка доступности сервера";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> GetPing()
    {
        if (await connector.Ping())
        {
            Console.WriteLine($"Название продукта: {connector.Product}");
            Console.WriteLine($"Версия сервера: {connector.ServerVersion}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
