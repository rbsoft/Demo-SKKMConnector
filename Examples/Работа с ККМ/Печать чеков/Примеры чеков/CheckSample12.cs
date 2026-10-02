using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample12 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все ставки НДС";

    public async Task<SkkmConnector> PostCheckSample12()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Товар без НДС", 1m, 100.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 100m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line2 = connector.AddPosition("Товар НДС 0%", 1m, 100.0m, TaxRate.Vat0, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 100m;
        line2.TaxSum = 0m;
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line3 = connector.AddPosition("Товар НДС 5%", 1m, 100.0m, TaxRate.Vat5, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line3.Price = 100m;
        line3.TaxSum = 0m;
        line3.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line4 = connector.AddPosition("Товар НДС 5/105", 1m, 100.0m, TaxRate.Vat5_105, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 100m;
        line4.TaxSum = 0m;
        line4.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line5 = connector.AddPosition("Товар НДС 7%", 1m, 100.0m, TaxRate.Vat7, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line5.Price = 100m;
        line5.TaxSum = 0m;
        line5.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line6 = connector.AddPosition("Товар НДС 7/107", 1m, 100.0m, TaxRate.Vat7_107, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line6.Price = 100m;
        line6.TaxSum = 0m;
        line6.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line7 = connector.AddPosition("Товар НДС 10%", 1m, 100.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line7.Price = 100m;
        line7.TaxSum = 0m;
        line7.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line8 = connector.AddPosition("Товар НДС 10/110", 1m, 100.0m, TaxRate.Vat10_110, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line8.Price = 100m;
        line8.TaxSum = 0m;
        line8.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line9 = connector.AddPosition("Товар НДС 18%", 1m, 100.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line9.Price = 100m;
        line9.TaxSum = 0m;
        line9.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line10 = connector.AddPosition("Товар НДС 18/118", 1m, 100.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line10.Price = 100m;
        line10.TaxSum = 0m;
        line10.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line11 = connector.AddPosition("Товар НДС 20%", 1m, 100.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line11.Price = 100m;
        line11.TaxSum = 0m;
        line11.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line12 = connector.AddPosition("Товар НДС 20/120", 1m, 100.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line12.Price = 100m;
        line12.TaxSum = 0m;
        line12.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line13 = connector.AddPosition("Товар НДС 22%", 1m, 100.0m, TaxRate.Vat22, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line13.Price = 100m;
        line13.TaxSum = 0m;
        line13.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line14 = connector.AddPosition("Товар НДС 22/122", 1m, 100.0m, TaxRate.Vat22_122, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line14.Price = 100m;
        line14.TaxSum = 0m;
        line14.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 1400;
        await connector.PrintCheck();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
