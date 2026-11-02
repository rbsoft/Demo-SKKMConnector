using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample53 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Частичная оплата чека";

    public async Task<SkkmConnector> PostCheckSample53()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 100m, 100.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        connector.Cash = 40m;
        connector.Credit = 60m;
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
