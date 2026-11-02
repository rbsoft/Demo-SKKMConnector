using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturn : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Печать чека возврата";

    public async Task<SkkmConnector> PostPrintCheckReturn()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Usn);
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.CustomerVatin = "500100732259";
        connector.Cash = 90;
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 60, 60, TaxRate.Vat20, SignCalculationObject.Advance, SignMethodCalculation.FullPrepayment);
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        var line2 = connector.AddPosition("SIMTEK манжета переходная D50х32", 1, 21.85m, 21.85m, TaxRate.None, SignCalculationObject.Advance, SignMethodCalculation.FullPrepayment);
        line2.DiscountSum = 0;
        line2.Department = 1;
        line2.TaxSum = 0;
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
