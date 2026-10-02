using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample36 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все признаки способа расчета";

    public async Task<SkkmConnector> PostCheckSample36()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Предоплата 100%", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line1.Price = 10m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line2 = connector.AddPosition("Предоплата", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.PartialPrepayment);
        line2.Price = 10m;
        line2.TaxSum = 0m;
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line3 = connector.AddPosition("Аванс", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.Advance);
        line3.Price = 10m;
        line3.TaxSum = 0m;
        line3.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line4 = connector.AddPosition("Полный расчёт", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 10m;
        line4.TaxSum = 0m;
        line4.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line5 = connector.AddPosition("Частичный расчёт", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.PartialPaymentAndCredit);
        line5.Price = 10m;
        line5.TaxSum = 0m;
        line5.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line6 = connector.AddPosition("Передача в кредит", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.CreditTransfer);
        line6.Price = 10m;
        line6.TaxSum = 0m;
        line6.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line7 = connector.AddPosition("Оплата кредита", 1m, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.CreditPayment);
        line7.Price = 10m;
        line7.TaxSum = 0m;
        line7.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 70.0m;
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
