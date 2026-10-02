using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturnAdvance : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Печать чека возврата аванса";

    public async Task<SkkmConnector> PostPrintCheckReturnAdvance()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.UsnDr);
        connector.NewCheck(CheckType.SaleReturn);
        connector.IsElectronically = false;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 10, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.Advance, signMethodCalculation: SignMethodCalculation.Advance);
        line1.Price = 10;
        line1.DiscountSum = 0;
        line1.Department = 2;
        line1.TaxSum = 0;
        connector.AdvancePayment = 10;
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
