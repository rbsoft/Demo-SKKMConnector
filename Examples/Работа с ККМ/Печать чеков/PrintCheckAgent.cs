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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition("Оплата услуги через агента", 1, 500, 500, TaxRate.Vat20,
            SignCalculationObject.Service, SignMethodCalculation.FullPayment);

        connector.AgentSign = AgentType.PaymentAgent;
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = new[] { "+74951234567" };
        connector.ReceivePaymentsOperatorPhone = new[] { "+74951234567" };
        connector.MoneyTransferOperatorName = "НКО \"Оператор\"";
        connector.MoneyTransferOperatorPhone = new[] { "+74957654321" };
        connector.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, 1";
        connector.MoneyTransferOperatorVatin = "7700000009";

        // Для признака агента 0–3 сервер требует поставщика: имя, телефон и ИНН.
        // Поля заданы в заголовке: на строку без своего Vendor сервер копирует их сам.
        connector.VendorDataName = "ООО Ромашка";
        connector.VendorDataPhones = new[] { "+74951234567" };
        connector.VendorDataVatin = "5262107639";

        connector.Cash = 500;

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
