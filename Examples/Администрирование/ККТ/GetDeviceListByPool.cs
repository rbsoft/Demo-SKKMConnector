using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetDeviceListByPool : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список ККТ по пулу";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetDeviceListByPool()
    {
        // Имя пула ККТ на сервере СККМ:
        connector.Pool = pool;
        if (await connector.GetDeviceListByPool())
        {
            foreach (var kkt in connector.DeviceList)
                Console.WriteLine($"Имя ККТ: {kkt.DeviceName} Драйвер: {kkt.Driver} Статус: {kkt.DeviceStatusDescription}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}