using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample37 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все значения мер количества";

    public async Task<SkkmConnector> PostCheckSample37()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 10m, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        var line2 = connector.AddPosition("Сахар 500г.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Gram);
        line2.TaxSum = 0m;
        var line3 = connector.AddPosition("Мука 0.5кг.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Kilogram);
        line3.TaxSum = 0m;
        var line4 = connector.AddPosition("Песок 0.5т.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Tonne);
        line4.TaxSum = 0m;
        var line5 = connector.AddPosition("Ткань 0.5см.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Centimeter);
        line5.TaxSum = 0m;
        var line6 = connector.AddPosition("Ткань 0.5дм.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Decimeter);
        line6.TaxSum = 0m;
        var line7 = connector.AddPosition("Ткань 0.5м.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Meter);
        line7.TaxSum = 0m;
        var line8 = connector.AddPosition("Плитка 0.5кв.см", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.SquareCentimeter);
        line8.TaxSum = 0m;
        var line9 = connector.AddPosition("Плитка 0.5кв.дм", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.SquareDecimeter);
        line9.TaxSum = 0m;
        var line10 = connector.AddPosition("Плитка 0.5кв.м", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.SquareMeter);
        line10.TaxSum = 0m;
        var line11 = connector.AddPosition("Масло 0.5мл.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Milliliter);
        line11.TaxSum = 0m;
        var line12 = connector.AddPosition("Молоко 0.5л.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Liter);
        line12.TaxSum = 0m;
        var line13 = connector.AddPosition("Газ 0.5м3", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.CubicMeter);
        line13.TaxSum = 0m;
        var line14 = connector.AddPosition("Электроэнергия", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.KilowattHour);
        line14.TaxSum = 0m;
        var line15 = connector.AddPosition("Тепло", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Gigacalorie);
        line15.TaxSum = 0m;
        var line16 = connector.AddPosition("Аренда, сутки", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Day);
        line16.TaxSum = 0m;
        var line17 = connector.AddPosition("Аренда, час", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Hour);
        line17.TaxSum = 0m;
        var line18 = connector.AddPosition("Аренда, мин.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Minute);
        line18.TaxSum = 0m;
        var line19 = connector.AddPosition("Аренда, сек.", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Second);
        line19.TaxSum = 0m;
        var line20 = connector.AddPosition("Файл, КБ", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Kilobyte);
        line20.TaxSum = 0m;
        var line21 = connector.AddPosition("Файл, МБ", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Megabyte);
        line21.TaxSum = 0m;
        var line22 = connector.AddPosition("Файл, ГБ", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Gigabyte);
        line22.TaxSum = 0m;
        var line23 = connector.AddPosition("Файл, ТБ", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Terabyte);
        line23.TaxSum = 0m;
        var line24 = connector.AddPosition("Прочее", 0.5m, 10m, 5.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Other);
        line24.TaxSum = 0m;
        connector.Cash = 125.0m;
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
