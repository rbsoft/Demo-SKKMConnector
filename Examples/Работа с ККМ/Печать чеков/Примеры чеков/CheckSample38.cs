using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample38 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Сведения об оплате безналичными";

    public async Task<SkkmConnector> PostCheckSample38()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.AddElectronicPaymentInfo(60m, ElectronicPaymentMethod.FullPayment, "RRN=123456789012", "Карта *1234");
        connector.AddElectronicPaymentInfo(40m, ElectronicPaymentMethod.FullPayment, "RRN=987654321000", "СБП");
        var line1 = connector.AddPosition("Вода 1л.", 1m, 100m, 100.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        connector.ElectronicPayment = 100m;
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
