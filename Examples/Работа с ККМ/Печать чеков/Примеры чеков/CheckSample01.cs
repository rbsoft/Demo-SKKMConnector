using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample01 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Продажа (базовый чек)";

    public async Task<SkkmConnector> PostCheckSample01()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Envd);
        connector.NewCheck(CheckType.Sale);

        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1m, 30m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 30;
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 5;
        line1.MeasurementUnit = "11";
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 30; 
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
