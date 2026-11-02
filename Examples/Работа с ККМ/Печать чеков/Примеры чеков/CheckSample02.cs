using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample02 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Подакцизный товар";

    public async Task<SkkmConnector> PostCheckSample02()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Usn);
        
        connector.IsElectronically = true;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1m, 10m, 10m, TaxRate.Vat20, SignCalculationObject.ExcisableGoods, SignMethodCalculation.FullPayment);
        line1.Department = 0;
        line1.TaxSum = 0m;
        line1.ExciseAmount = 1m;
        var line2 = connector.AddPosition("Пиво", 3m, 10.50m, 31.50m, TaxRate.Vat20, SignCalculationObject.ExcisableGoods, SignMethodCalculation.FullPayment);
        line2.Department = 1;
        line2.TaxSum = 0m;
        connector.Cash = 100.01m;

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
