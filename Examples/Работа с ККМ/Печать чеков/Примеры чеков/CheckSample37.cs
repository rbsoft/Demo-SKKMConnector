using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample37 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все значения мер количества";

    public async Task<SkkmConnector> PostCheckSample37()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 10m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line2 = connector.AddPosition("Сахар 500г.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 10m;
        line2.TaxSum = 0m;
        line2.MeasureOfQuantity = MeasureOfQuantity.Gram;
        var line3 = connector.AddPosition("Мука 0.5кг.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line3.Price = 10m;
        line3.TaxSum = 0m;
        line3.MeasureOfQuantity = MeasureOfQuantity.Kilogram;
        var line4 = connector.AddPosition("Песок 0.5т.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 10m;
        line4.TaxSum = 0m;
        line4.MeasureOfQuantity = MeasureOfQuantity.Tonne;
        var line5 = connector.AddPosition("Ткань 0.5см.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line5.Price = 10m;
        line5.TaxSum = 0m;
        line5.MeasureOfQuantity = MeasureOfQuantity.Centimeter;
        var line6 = connector.AddPosition("Ткань 0.5дм.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line6.Price = 10m;
        line6.TaxSum = 0m;
        line6.MeasureOfQuantity = MeasureOfQuantity.Decimeter;
        var line7 = connector.AddPosition("Ткань 0.5м.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line7.Price = 10m;
        line7.TaxSum = 0m;
        line7.MeasureOfQuantity = MeasureOfQuantity.Meter;
        var line8 = connector.AddPosition("Плитка 0.5кв.см", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line8.Price = 10m;
        line8.TaxSum = 0m;
        line8.MeasureOfQuantity = MeasureOfQuantity.SquareCentimeter;
        var line9 = connector.AddPosition("Плитка 0.5кв.дм", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line9.Price = 10m;
        line9.TaxSum = 0m;
        line9.MeasureOfQuantity = MeasureOfQuantity.SquareDecimeter;
        var line10 = connector.AddPosition("Плитка 0.5кв.м", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line10.Price = 10m;
        line10.TaxSum = 0m;
        line10.MeasureOfQuantity = MeasureOfQuantity.SquareMeter;
        var line11 = connector.AddPosition("Масло 0.5мл.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line11.Price = 10m;
        line11.TaxSum = 0m;
        line11.MeasureOfQuantity = MeasureOfQuantity.Milliliter;
        var line12 = connector.AddPosition("Молоко 0.5л.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line12.Price = 10m;
        line12.TaxSum = 0m;
        line12.MeasureOfQuantity = MeasureOfQuantity.Liter;
        var line13 = connector.AddPosition("Газ 0.5м3", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line13.Price = 10m;
        line13.TaxSum = 0m;
        line13.MeasureOfQuantity = MeasureOfQuantity.CubicMeter;
        var line14 = connector.AddPosition("Электроэнергия", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line14.Price = 10m;
        line14.TaxSum = 0m;
        line14.MeasureOfQuantity = MeasureOfQuantity.KilowattHour;
        var line15 = connector.AddPosition("Тепло", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line15.Price = 10m;
        line15.TaxSum = 0m;
        line15.MeasureOfQuantity = MeasureOfQuantity.Gigacalorie;
        var line16 = connector.AddPosition("Аренда, сутки", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line16.Price = 10m;
        line16.TaxSum = 0m;
        line16.MeasureOfQuantity = MeasureOfQuantity.Day;
        var line17 = connector.AddPosition("Аренда, час", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line17.Price = 10m;
        line17.TaxSum = 0m;
        line17.MeasureOfQuantity = MeasureOfQuantity.Hour;
        var line18 = connector.AddPosition("Аренда, мин.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line18.Price = 10m;
        line18.TaxSum = 0m;
        line18.MeasureOfQuantity = MeasureOfQuantity.Minute;
        var line19 = connector.AddPosition("Аренда, сек.", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line19.Price = 10m;
        line19.TaxSum = 0m;
        line19.MeasureOfQuantity = MeasureOfQuantity.Second;
        var line20 = connector.AddPosition("Файл, КБ", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line20.Price = 10m;
        line20.TaxSum = 0m;
        line20.MeasureOfQuantity = MeasureOfQuantity.Kilobyte;
        var line21 = connector.AddPosition("Файл, МБ", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line21.Price = 10m;
        line21.TaxSum = 0m;
        line21.MeasureOfQuantity = MeasureOfQuantity.Megabyte;
        var line22 = connector.AddPosition("Файл, ГБ", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line22.Price = 10m;
        line22.TaxSum = 0m;
        line22.MeasureOfQuantity = MeasureOfQuantity.Gigabyte;
        var line23 = connector.AddPosition("Файл, ТБ", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line23.Price = 10m;
        line23.TaxSum = 0m;
        line23.MeasureOfQuantity = MeasureOfQuantity.Terabyte;
        var line24 = connector.AddPosition("Прочее", 0.5m, 5.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line24.Price = 10m;
        line24.TaxSum = 0m;
        line24.MeasureOfQuantity = MeasureOfQuantity.Other;
        connector.Cash = 125.0m;
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
