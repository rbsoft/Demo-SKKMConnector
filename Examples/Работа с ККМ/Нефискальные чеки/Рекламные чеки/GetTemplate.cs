using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Рекламные чеки";
    public const string Title = "Получение шаблона печати";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetGetTemplate()
    {
        await connector.GetTemplate("Template32");
        if (connector.Ok)
        {
            Console.WriteLine($"Шаблон: {connector.PrintTemplateName}, тип={connector.PrintTemplateType}, строк={connector.PrintTemplateLineCount}");
            foreach (var line in connector.TemplateLines)
            {
                var text = string.IsNullOrEmpty(line.LineRight)
                    ? line.Line
                    : $"{line.Line} | {line.LineRight}";
                Console.WriteLine(
                    $"  [{line.Type}] {text} (шрифт={line.Font}, выравнивание={line.Alignment}, " +
                    $"ширина={line.Width}, масштаб={line.Scale}, перенос={line.Wrap})");
                if (line.BarcodeType != null)
                    Console.WriteLine($"    штрихкод: {line.BarcodeType}, {line.BarcodeValue}, высота={line.BarcodeHeight}, ширина штриха={line.BarcodeBarWidth}");
                if (line.PictureName != null || line.PictureWidth > 0)
                    Console.WriteLine($"    картинка: {line.PictureName} ({line.PictureWidth}x{line.PictureHeight})");
            }
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
