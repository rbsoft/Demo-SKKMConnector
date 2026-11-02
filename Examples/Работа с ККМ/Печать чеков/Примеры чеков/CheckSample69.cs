using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample69 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Импортный маркированный подакцизный товар с таможенной декларацией";

    public async Task<SkkmConnector> PostCheckSample69()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Коньяк 0.5л.", 1m, 2480m, 2480.0m, TaxRate.Vat20, SignCalculationObject.ExcisableMarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 413.33m;
        line1.ExciseAmount = 185.4m;
        line1.CountryOfOrigin = "276";
        line1.CustomsDeclaration = "10009100/140923/0001122";
        line1.Cis_Base64 = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        connector.Cash = 2480.0m;
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
