using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample12 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все ставки НДС";

    public async Task<SkkmConnector> PostCheckSample12()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Товар без НДС", 1m, 100m, 100.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        var line2 = connector.AddPosition("Товар НДС 0%", 1m, 100m, 100.0m, TaxRate.Vat0, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line2.TaxSum = 0m;
        var line3 = connector.AddPosition("Товар НДС 5%", 1m, 100m, 100.0m, TaxRate.Vat5, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line3.TaxSum = 0m;
        var line4 = connector.AddPosition("Товар НДС 5/105", 1m, 100m, 100.0m, TaxRate.Vat5_105, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line4.TaxSum = 0m;
        var line5 = connector.AddPosition("Товар НДС 7%", 1m, 100m, 100.0m, TaxRate.Vat7, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line5.TaxSum = 0m;
        var line6 = connector.AddPosition("Товар НДС 7/107", 1m, 100m, 100.0m, TaxRate.Vat7_107, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line6.TaxSum = 0m;
        var line7 = connector.AddPosition("Товар НДС 10%", 1m, 100m, 100.0m, TaxRate.Vat10, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line7.TaxSum = 0m;
        var line8 = connector.AddPosition("Товар НДС 10/110", 1m, 100m, 100.0m, TaxRate.Vat10_110, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line8.TaxSum = 0m;
        var line9 = connector.AddPosition("Товар НДС 18%", 1m, 100m, 100.0m, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line9.TaxSum = 0m;
        var line10 = connector.AddPosition("Товар НДС 18/118", 1m, 100m, 100.0m, TaxRate.Vat20_120, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line10.TaxSum = 0m;
        var line11 = connector.AddPosition("Товар НДС 20%", 1m, 100m, 100.0m, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line11.TaxSum = 0m;
        var line12 = connector.AddPosition("Товар НДС 20/120", 1m, 100m, 100.0m, TaxRate.Vat20_120, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line12.TaxSum = 0m;
        var line13 = connector.AddPosition("Товар НДС 22%", 1m, 100m, 100.0m, TaxRate.Vat22, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line13.TaxSum = 0m;
        var line14 = connector.AddPosition("Товар НДС 22/122", 1m, 100m, 100.0m, TaxRate.Vat22_122, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line14.TaxSum = 0m;
        connector.Cash = 1400;
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
