using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample40 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все виды штрихкода";

    public async Task<SkkmConnector> PostCheckSample40()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 10m;
        line1.TaxSum = 0m;
        connector.AddBarcode(BarcodeType.QR, "https://shop.ru/check", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN13, "4601234567893", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN8, "96385074", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE128, "ABC-12345", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE39, "CODE39-TEST", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE93, "CODE93", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.PDF417, "pdf417-payload", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.UPCA, "012345678905", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.UPCE, "04252614", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.ITF, "1234567890", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODABAR, "A123456A", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE32, "01234567", PrintAlignment.Center);
        connector.Cash = 10.0m;
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
