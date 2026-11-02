using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetDeviceListByPool : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список ККТ по пулу";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetDeviceListByPool()
    {
        connector.Pool = pool;
        if (await connector.GetDeviceListByPool())
        {
                foreach (var kkt in connector.DeviceList)
                {
                    Console.WriteLine($"Имя ККМ: {kkt.DeviceName}");
                    Console.WriteLine($"Тип драйвера: {kkt.Driver}");
                    Console.WriteLine($"Описание статуса: {kkt.DeviceStatusDescription}");
                }

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
