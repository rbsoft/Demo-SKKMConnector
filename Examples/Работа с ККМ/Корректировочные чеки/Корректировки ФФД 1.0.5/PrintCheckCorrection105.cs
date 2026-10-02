using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection105 : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Печать чека коррекции 1.0.5";

    public async Task<SkkmConnector> PostPrintCheckCorrection105()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);

        connector.NewCheckCorrection(cashierName, CheckType.CorrectionSale, TaxSystem.Osn,
            CorrectionTypes.Самостоятельно, "Основание коррекции", DateTime.Today);
        connector.CorrectionSumTax20 = 16.67m;
        var line1 = connector.AddPosition("Товар по коррекции 1.05", 1, 100, TaxRate.Vat20);
        line1.Price = 100;
        line1.TaxSum = 16.67m;
        connector.Cash = 100;
        await connector.PrintCheckCorrection105();
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
