using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetPicture : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Работа с картинками";
    public const string Title = "Получение изображения";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetPicture()
    {
        connector.DeviceName = deviceName;
        await connector.GetPicture("demo.bmp");
        if (connector.Ok)
            Console.WriteLine(
                "Картинка получена.\n" +
                $"Имя: {connector.PictureId}\n" +
                $"Размер Base64: {connector.PictureBase64Result.Length} симв.\n" +
                $"Base64: {connector.PictureBase64Result}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
