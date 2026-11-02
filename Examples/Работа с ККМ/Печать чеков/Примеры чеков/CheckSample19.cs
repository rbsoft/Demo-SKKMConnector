using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample19 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Дробное количество маркированного товара";

    public async Task<SkkmConnector> PostCheckSample19()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Usn);
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1.234m, 750.0m, 750.0m, TaxRate.Vat20, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment);
        line1.Department = 2;
        line1.TaxSum = 0m;
        line1.Cis_Base64 = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        line1.FractionalQuantityNumerator = 1;
        line1.FractionalQuantityDenominator = 208;
        connector.Cash = 750;
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
