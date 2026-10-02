using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class UpdateTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Рекламные чеки";
    public const string Title = "Редактирование шаблона печати";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PutUpdateTemplate()
    {
        connector.NewTemplate("Template32", PrintTemplateType.Advertisement);

        connector.AddText("Текс222т", PrintFont.H1, PrintAlignment.Center);
        connector.AddText("Сумма", "1 250,00");
        connector.AddSeparatorLine();
        connector.AddBarcode(BarcodeType.QR, "https://www.rbsoft.ru/", PrintAlignment.Center, height: 30, barWidth: 6);
        connector.AddPicture(filePath, PictureAlignment.Center, 200, 80);

        await connector.UpdateTemplate();
        if (connector.Ok)
            Console.WriteLine($"Шаблон обновлён: {connector.TemplateName}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
