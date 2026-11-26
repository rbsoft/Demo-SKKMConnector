using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Приход (все штрихкоды)
/// </summary>
public class CheckSample40 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Штрихкоды";
    public const int SortOrder = 29;

    public async Task<SkkmConnector> PostCheckSample40()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition(
            "Вода 1л.",
            1m,
            1m,
            1m,
            TaxRate.Vat0,
            SignCalculationObject.Goods,
            SignMethodCalculation.FullPayment,
            MeasureOfQuantity.Piece);

        // Значения и типы — как в демо 1С (QR передан строкой Base64)
        connector.AddBarcode(BarcodeType.QR, "aHR0cHM6Ly9zaG9wLnJ1L2NoZWNr", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN13, "4601234567890", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.EAN8, "46012346", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE128, "ABC-12345", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE39, "CODE39-TEST", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE93, "CODE93", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.PDF417, "pdf417-payload", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.UPCA, "012345678905", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.UPCE, "01234565", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.ITF, "1234567890", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODABAR, "A123456A", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.CODE32, "01234567", PrintAlignment.Center);

        connector.Cash = 1m;

        if (await connector.PrintCheck())
        {

            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
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
