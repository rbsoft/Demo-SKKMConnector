using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetPrintForm : Sample
{
    public const string GroupPath = "Служебные";
    public const string Title = "Печатная форма чека";
    public const int SortOrder = 3;
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetGetPrintForm()
    {
        connector.DeviceName = deviceName;
        await connector.GetPrintForm(documentId);
        if (connector.Ok)
        {
            Console.WriteLine($"Строк печатной формы: {connector.PrintForm.Count}");
            var index = 0;
            foreach (var line in connector.PrintForm)
            {
                index++;
                Console.WriteLine(
                    $"  #{index} [{line.Type}] выравнивание={line.Alignment}, шрифт={line.Font} (задан={line.IsFontSpecified}), " +
                    $"ширина={line.Width}, масштаб={line.Scale}%, перенос={line.Wrap}, из шаблона={line.IsCreateFromTemplate}");

                if (!string.IsNullOrEmpty(line.Line) || !string.IsNullOrEmpty(line.LineRight))
                    Console.WriteLine($"      слева: \"{line.Line}\" | справа: \"{line.LineRight}\"");

                if (line.SeparatorLineStyle != null)
                    Console.WriteLine($"      разделитель: стиль={line.SeparatorLineStyle}");

                if (line.BarcodeType != null)
                {
                    Console.WriteLine(
                        $"      штрихкод: тип={line.BarcodeType}, значение=\"{line.BarcodeValue}\", " +
                        $"подпись={line.BarcodePrintText}, высота={line.BarcodeHeight}, ширина штриха={line.BarcodeBarWidth}, " +
                        $"картинка(Base64)={(string.IsNullOrEmpty(line.BarcodePictureBase64) ? "нет" : $"{line.BarcodePictureBase64!.Length} симв.")}");
                    if (line.BarcodeLines.Count > 0)
                        foreach (var bl in line.BarcodeLines)
                            Console.WriteLine($"        {bl}");
                }
            }
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
