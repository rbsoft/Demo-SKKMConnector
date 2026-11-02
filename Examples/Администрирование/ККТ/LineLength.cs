using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class LineLength : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Ширина строки";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> GetLineLength()
    {
        connector.DeviceName = deviceName;
        if (await connector.GetLineLength())
        {
            Console.WriteLine($"Ширина строки в символах: {connector.DeviceLineLength}");
            Console.WriteLine($"Ширина строки в пикселях: {connector.DeviceLineLengthPixels}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}