using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample47 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все способы электронной оплаты (0-6)";

    public async Task<SkkmConnector> PostCheckSample47()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.FullPrepayment, "M=0", "Способ 0");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.PartialPrepayment, "M=1", "Способ 1");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.Advance, "M=2", "Способ 2");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.FullPayment, "M=3", "Способ 3");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.PartialPaymentAndCredit, "M=4", "Способ 4");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.CreditTransfer, "M=5", "Способ 5");
        connector.AddElectronicPayment(10m, ElectronicPaymentMethod.CreditPayment, "M=6", "Способ 6");

        var line1 = connector.AddPosition("Вода 1л.", 1m, 70.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 70m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.ElectronicPayment = 70m;
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
