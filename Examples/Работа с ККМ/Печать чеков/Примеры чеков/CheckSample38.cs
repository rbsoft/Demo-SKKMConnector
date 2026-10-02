using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample38 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Сведения об оплате безналичными";

    public async Task<SkkmConnector> PostCheckSample38()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        connector.ElectronicPaymentAmount = 60;
        connector.ElectronicPaymentMethod = ElectronicPaymentMethod.FullPayment;
        connector.ElectronicPaymentIdentifiers = "RRN=123456789012";
        connector.ElectronicPaymentAdditionalInformation = "Карта *1234";

        connector.ElectronicPaymentAmount = 40m;
        connector.ElectronicPaymentMethod = ElectronicPaymentMethod.FullPayment;
        connector.ElectronicPaymentIdentifiers = "RRN=987654321000";
        connector.ElectronicPaymentAdditionalInformation = "СБП";
        var line1 = connector.AddPosition("Вода 1л.", 1m, 100.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 100m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.ElectronicPayment = 100m;
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
