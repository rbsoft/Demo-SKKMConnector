using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturnAdvance : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Печать чека возврата аванса";

    public async Task<SkkmConnector> PostPrintCheckReturnAdvance()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.SaleReturn, TaxSystem.UsnDr);
        connector.IsElectronically = false;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 10, 10, TaxRate.Vat20_120, SignCalculationObject.Advance, SignMethodCalculation.Advance);
        line1.DiscountSum = 0;
        line1.Department = 2;
        line1.TaxSum = 0;
        connector.AdvancePayment = 10;
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
