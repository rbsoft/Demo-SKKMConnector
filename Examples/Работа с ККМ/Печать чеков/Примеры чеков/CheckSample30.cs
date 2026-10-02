using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample30 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Позиция с акцизом, страной происхождения, таможенной декларацией и скидкой";

    public async Task<SkkmConnector> PostCheckSample30()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Сигареты", 2m, 90m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.ExcisableGoods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.ProductCode = "2402 20 900 0";
        line1.Price = 50m;
        line1.DiscountSum = 10m;
        line1.Department = 1;
        line1.TaxSum = 15m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line1.ExciseAmount = 5.5m;
        line1.CountryOfOrigin = "643";
        line1.CustomsDeclaration = "10009100/220211/0001122";
        line1.AdditionalAttribute = "Доп. реквизит";
        connector.Cash = 90.0m;
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
