using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample42 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Чек с нефискальными строками";

    public async Task<SkkmConnector> PostCheckSample42()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.Clear();
        connector.PaymentType = CheckType.Sale;
        connector.SetTaxType(TaxSystem.Osn);
        connector.AddText("Заголовок чека", PrintFont.H1, PrintAlignment.Center);
        connector.AddSeparatorLine(LineStyle.Solid);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 50.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 50m;
        line1.TaxSum = 8.33m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.AddText("Спасибо за покупку", PrintFont.Small, PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.QR, "https://shop.ru/loyalty", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN13, "4601234567890", PrintAlignment.Left);
        var line12 = connector.AddPicture(filePath, PictureAlignment.Center);
        line12.Width = 1;
        line12.Height = 1;
        connector.AddSeparatorLine(LineStyle.Bold);
        connector.AddSeparatorLine(LineStyle.Dashed);
        connector.AddSeparatorLine(LineStyle.Dotted);
        connector.AddSeparatorLine(LineStyle.Double);
        connector.AddText("https://nalog.gov.ru");
        connector.Cash = 50.0m;
        await connector.PrintCheck();
        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
