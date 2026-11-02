using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample03 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Часовая зона";

    public async Task<SkkmConnector> PostCheckSample03()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.TimeZone = CheckTimeZone.MskPlus7;

        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 2m, 50m, 100m, TaxRate.Vat20, SignCalculationObject.Advance, SignMethodCalculation.Advance, MeasureOfQuantity.KilowattHour);
        line1.Department = 0;
        line1.TaxSum = 0m;
        connector.Cash = 50m;
        connector.ElectronicPayment = 90.00m;
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
