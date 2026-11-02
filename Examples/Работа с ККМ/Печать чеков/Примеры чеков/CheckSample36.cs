using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample36 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все признаки способа расчета";

    public async Task<SkkmConnector> PostCheckSample36()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Предоплата 100%", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPrepayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        var line2 = connector.AddPosition("Предоплата", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.PartialPrepayment, MeasureOfQuantity.Piece);
        line2.TaxSum = 0m;
        var line3 = connector.AddPosition("Аванс", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.Advance, MeasureOfQuantity.Piece);
        line3.TaxSum = 0m;
        var line4 = connector.AddPosition("Полный расчёт", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line4.TaxSum = 0m;
        var line5 = connector.AddPosition("Частичный расчёт", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line5.TaxSum = 0m;
        var line6 = connector.AddPosition("Передача в кредит", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.CreditTransfer, MeasureOfQuantity.Piece);
        line6.TaxSum = 0m;
        var line7 = connector.AddPosition("Оплата кредита", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.CreditPayment, MeasureOfQuantity.Piece);
        line7.TaxSum = 0m;
        connector.Cash = 70.0m;
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
