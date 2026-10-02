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
        await connector.GetLineLength();

        if (connector.Ok)
            Console.WriteLine($"Ширина строки: {connector.LineLength}, пикселей: {connector.LineLengthPixels}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}