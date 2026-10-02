using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class SendPicture : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Работа с картинками";
    public const string Title = "Загрузить картинку";
    public const bool NeedFilePath = true;
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostSendPicture()
    {
        connector.DeviceName = deviceName;
        connector.PictureAlignment = PictureAlignment.Center;
        // Путь берётся из панели подключения; коннектор сам читает файл,
        // проверяет формат (BMP/PNG) и кодирует в Base64.
        await connector.SendPicture(filePath);

        if (connector.Ok)
            Console.WriteLine($"Картинка загружена: {filePath}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
