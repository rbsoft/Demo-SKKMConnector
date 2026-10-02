using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrectionPurchase : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Коррекция расхода 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrectionPurchase()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);

        connector.NewCheckCorrection(cashierName, CheckType.CorrectionPurchase, TaxSystem.Osn,
            CorrectionTypes.Самостоятельно, "Коррекция расхода", DateTime.Today);
        var line1 = connector.AddPosition("Товар по коррекции расхода", 1, 100, TaxRate.Vat20);
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
