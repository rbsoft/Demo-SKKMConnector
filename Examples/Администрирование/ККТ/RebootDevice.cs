using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class RebootDevice : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Перезапуск ККТ";
    public const int SortOrder = 10;

    public async Task<SkkmConnector> PostRebootDevice()
    {
        connector.DeviceName = deviceName;
        await connector.RebootDevice();
        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
