using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetDeviceListByPool : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список ККТ по пулу";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetDeviceListByPool()
    {
        connector.PoolName = "pool";
        await connector.GetDeviceListByPool();
        if (connector.Ok)
        {
            Console.WriteLine($"Устройств в пуле: {connector.Devices.Length}");
            foreach (var d in connector.Devices)
                Console.WriteLine($"  {d.DeviceName} [{d.Driver}], пул={d.Pool}, {d.DeviceStatusDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
