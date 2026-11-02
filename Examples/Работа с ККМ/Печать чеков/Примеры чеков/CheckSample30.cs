using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample30 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Позиция с акцизом, страной происхождения, таможенной декларацией и скидкой";

    public async Task<SkkmConnector> PostCheckSample30()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Сигареты", 2m, 50m, 90m, TaxRate.Vat20, SignCalculationObject.ExcisableGoods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.DiscountSum = 10m;
        line1.Department = 1;
        line1.TaxSum = 15m;
        line1.ExciseAmount = 5.5m;
        line1.CountryOfOrigin = "643";
        line1.CustomsDeclaration = "10009100/220211/0001122";
        line1.AdditionalAttribute = "Доп. реквизит";
        connector.Cash = 90.0m;
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
