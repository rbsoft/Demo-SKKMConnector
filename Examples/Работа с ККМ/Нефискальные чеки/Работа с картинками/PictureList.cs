using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PictureList : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Работа с картинками";
    public const string Title = "Список картинок";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetPictureList()
    {
        connector.DeviceName = deviceName;
        await connector.GetPictureList();

        if (connector.Ok)
        {
            Console.WriteLine($"Картинок: {connector.Pictures.Count}");
            foreach (var p in connector.Pictures)
                Console.WriteLine(
                    $"  {p.PictureName} ({p.Width}x{p.Height}), выравнивание={p.Alignment}, " +
                    $"строки {p.StartLineNumber}–{p.EndLineNumber}, загружена={p.IsUploaded}, перезапись={p.Override}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}