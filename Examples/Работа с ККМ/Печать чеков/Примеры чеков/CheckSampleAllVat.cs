using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Приход со всеми ставками НДС в одном чеке — как в демо 1С.
/// </summary>
public class CheckSampleAllVat : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все ставки НДС";
    public const int SortOrder = 10;

    public async Task<SkkmConnector> PostCheckSampleAllVat()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition("Товар без НДС", 1m, 10m, 10m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 0%", 1m, 10m, 10m, TaxRate.Vat0, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 5%", 1m, 10m, 10m, TaxRate.Vat5, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 5/105", 1m, 10m, 10m, TaxRate.Vat5_105, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 7%", 1m, 10m, 10m, TaxRate.Vat7, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 7/107", 1m, 10m, 10m, TaxRate.Vat7_107, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 10%", 1m, 10m, 10m, TaxRate.Vat10, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 10/110", 1m, 10m, 10m, TaxRate.Vat10_110, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 18%", 1m, 10m, 10m, TaxRate.Vat18, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 18/118", 1m, 10m, 10m, TaxRate.Vat18_118, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 20%", 1m, 10m, 10m, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 20/120", 1m, 10m, 10m, TaxRate.Vat20_120, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 22%", 1m, 10m, 10m, TaxRate.Vat22, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.AddPosition("Товар НДС 22/122", 1m, 10m, 10m, TaxRate.Vat22_122, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);

        connector.Cash = 140m; // 14 × 10

        if (await connector.PrintCheck())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
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
