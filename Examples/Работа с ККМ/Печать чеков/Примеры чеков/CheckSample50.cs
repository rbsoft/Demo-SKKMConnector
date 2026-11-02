using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample50 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Товар со страной происхождения и номером таможенной декларации";

    public async Task<SkkmConnector> PostCheckSample50()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Товар импортный", 1m, 100m, 100.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        line1.CountryOfOrigin = "276";
        line1.CustomsDeclaration = "10009100/140923/0001122";
        connector.Cash = 100.0m;
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
