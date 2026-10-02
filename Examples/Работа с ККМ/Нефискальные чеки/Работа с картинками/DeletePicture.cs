using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class DeletePicture : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Работа с картинками";
    public const string Title = "Удаление изображения";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> DeleteDeletePicture()
    {
        connector.DeviceName = deviceName;
        connector.PictureId = "demo.bmp";
        await connector.DeletePicture();
        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
