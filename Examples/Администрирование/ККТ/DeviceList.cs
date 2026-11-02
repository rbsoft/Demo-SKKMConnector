using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class DeviceList : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Список ККМ";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> GetDeviceList()
    {
        if (await connector.GetDeviceList())
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