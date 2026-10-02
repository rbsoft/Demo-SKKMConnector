using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample22 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Данные платёжного агента и поставщика в заголовке";

    public async Task<SkkmConnector> PostCheckSample22()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        connector.AgentSign = AgentType.PaymentAgent;
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = new[] { "+79001234567" };
        connector.ReceivePaymentsOperatorPhone = new[] { "+79001234567" };
        connector.VendorName = "ООО Ромашка";
        connector.VendorPhones = new[] { "+79001234567" };
        connector.VendorVatin = "7701234560";
        var line1 = connector.AddPosition("Услуга", 1m, 90.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 90m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line1.AgentSign = AgentType.PaymentAgent;
        connector.Cash = 90;
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
