using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintSlip : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Печать нефискального документа";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostPrintSlip()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Новый документ: тип — приход, СНО — общая:
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        // Многострочный текст. Префиксы строк (имеют приоритет перед Font и Alignment):
        // [big], [small], [bold], [H1]–[H5], [left], [right], [center], [QR], [line]:
        connector.Text = "[H2]Это заголовок H2\n" +
                         "[left]Вторая строка слева\n" +
                         "[center]Третья строка по центру\n" +
                         "[right]Четвёртая строка справа";

        // Добавляем текст (текст, шрифт, выравнивание):
        connector.AddText("[big]Подпись к позиции", PrintFont.Normal, PrintAlignment.Left);

        // Добавляем штрихкод (тип, значение, выравнивание):
        connector.AddBarcode(BarcodeType.QR, "https://www.rbsoft.ru");

        // Картинка по центру. Файл берётся из поля «Файл» и уходит в Base64.
        // На Shtrih ширина не больше ширины ленты (320 px).
        connector.AddPicture(Convert.ToBase64String(File.ReadAllBytes(filePath)), PictureAlignment.Center);

        // Печатаем слип-чек:
        if (await connector.PrintSlip())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}