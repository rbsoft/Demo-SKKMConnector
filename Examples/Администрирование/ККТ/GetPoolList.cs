using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetPoolList : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список пулов";
    public const int SortOrder = 12;

    public async Task<SkkmConnector> GetGetPoolList()
    {
        await connector.GetPoolList();
        if (connector.Ok)
        {
            Console.WriteLine($"Пулов: {connector.Pools.Length}");
            foreach (var p in connector.Pools)
                Console.WriteLine($"  {p}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
