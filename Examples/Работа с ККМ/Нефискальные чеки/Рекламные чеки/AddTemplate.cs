using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class AddTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Рекламные чеки";
    public const string Title = "Создание шаблона печати";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostAddTemplate()
    {
        connector.NewTemplate("Template32", PrintTemplateType.CheckLines);

        connector.AddText("Текст", "Информация", PrintFont.H1);
        connector.AddText("Сумма", "1 250,00");
        connector.AddSeparatorLine();
        connector.AddBarcode(BarcodeType.QR, "https://www.rbsoft.ru/", PrintAlignment.Center, height: 30, barWidth: 6);
        connector.AddPicture(filePath, PictureAlignment.Center, 200, 80);

        await connector.AddTemplate();
        if (connector.Ok)
            Console.WriteLine($"Шаблон сохранён: {connector.TemplateName}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
