using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturn : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Печать чека возврата";

    public async Task<SkkmConnector> PostPrintCheckReturn()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Usn);
        connector.NewCheck(CheckType.Sale);
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.CustomerVatin = "500100732259";
        connector.Cash = 90;
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 60, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Advance, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line1.MeasurementUnit = "шт";
        line1.Price = 60;
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        var line2 = connector.AddPosition("SIMTEK манжета переходная D50х32", 1, 21.85m, TaxRate.None, signCalculationObject: SignCalculationObject.Advance, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line2.MeasurementUnit = "шт";
        line2.Price = 21.85m;
        line2.DiscountSum = 0;
        line2.Department = 1;
        line2.TaxSum = 0;
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
