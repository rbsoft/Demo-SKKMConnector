using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample46 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Коррекция малой суммы с распределением по нескольким видам оплаты";

    public async Task<SkkmConnector> PostCheckSample46()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Вода 1л.", 3m, 0.04m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 0.01m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line2 = connector.AddPosition("Хлеб", 1.7m, 0.05m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 0.03m;
        line2.TaxSum = 0m;
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line3 = connector.AddPosition("Молоко 1л.", 0.333m, 0.1m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line3.Price = 0.3m;
        line3.TaxSum = 0m;
        line3.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line4 = connector.AddPosition("Сок 1л.", 11m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 0.09m;
        line4.TaxSum = 0m;
        line4.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 1.19m;
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
