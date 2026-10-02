using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class DeviceList : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список ККМ";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> GetDeviceList()
    {
        await connector.GetDeviceList();

        if (connector.Ok)
        {
            Console.WriteLine($"Устройств: {connector.Devices.Length}");
            foreach (var d in connector.Devices)
                Console.WriteLine($"  {d.DeviceName} [{d.Driver}], пул={d.Pool}, {d.DeviceStatusDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}