using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Коррекция прихода 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrection()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.CorrectionSale);
        connector.CorrectionType = CorrectionTypes.Самостоятельно;
        connector.CorrectionDescription = "Коррекция расчёта";
        connector.CorrectionDate = DateTime.Today;
        var line1 = connector.AddPosition("Товар по коррекции", 1, 100, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 100;
        connector.Cash = 100;
        await connector.PrintCheckCorrection120();
        if (connector.Ok)
            Console.WriteLine(
                "Чек коррекции пробит.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Время ККТ: {connector.DeviceDateTime}\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
