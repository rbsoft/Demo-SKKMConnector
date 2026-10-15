using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckAgent : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Печать чека с агентом";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostPrintCheckAgent()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);

        connector.NewCheck(CheckType.Sale);

        connector.AddPosition("Оплата услуги через агента", 1, 500, TaxRate.Vat20,
            SignCalculationObject.Service, SignMethodCalculation.FullPayment);

        connector.AgentSign = AgentType.PaymentAgent;
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = new[] { "+74951234567" };
        connector.ReceivePaymentsOperatorPhone = new[] { "+74951234567" };
        connector.MoneyTransferOperatorName = "НКО \"Оператор\"";
        connector.MoneyTransferOperatorPhone = new[] { "+74957654321" };
        connector.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, 1";
        connector.MoneyTransferOperatorVatin = "7700000000";

        connector.Cash = 500;

        await connector.PrintCheck();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Время ККТ: {connector.DeviceDateTime}\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
