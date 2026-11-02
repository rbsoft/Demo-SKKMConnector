using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample42 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Чек с нефискальными строками";

    public async Task<SkkmConnector> PostCheckSample42()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.AddText("Заголовок чека", PrintFont.H1, PrintAlignment.Center);
        connector.AddSeparatorLine(LineStyle.Solid);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 50m, 50.0m, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 8.33m;
        connector.AddText("Спасибо за покупку", PrintFont.Small, PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.QR, "https://shop.ru/loyalty", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN13, "4601234567890", PrintAlignment.Left);
        connector.AddPicture(Convert.ToBase64String(File.ReadAllBytes(filePath)), PictureAlignment.Center);
        connector.AddSeparatorLine(LineStyle.Bold);
        connector.AddSeparatorLine(LineStyle.Dashed);
        connector.AddSeparatorLine(LineStyle.Dotted);
        connector.AddSeparatorLine(LineStyle.Double);
        connector.AddText("https://nalog.gov.ru");
        connector.Cash = 50.0m;
        if (await connector.PrintCheck())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"ФП чека: {connector.FiscalSign}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
