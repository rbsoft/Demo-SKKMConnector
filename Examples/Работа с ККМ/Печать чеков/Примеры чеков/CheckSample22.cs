using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample22 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Данные платёжного агента и поставщика в заголовке";

    public async Task<SkkmConnector> PostCheckSample22()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.AgentSign = AgentType.PaymentAgent;
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = new[] { "+79001234567" };
        connector.ReceivePaymentsOperatorPhone = new[] { "+79001234567" };
        connector.VendorDataName = "ООО Ромашка";
        connector.VendorDataPhones = new[] { "+79001234567" };
        connector.VendorDataVatin = "7701234560";
        var line1 = connector.AddPosition("Услуга", 1m, 90m, 90.0m, TaxRate.None, SignCalculationObject.Service, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        line1.SignSubjectCalculationAgent = AgentType.PaymentAgent;
        connector.Cash = 90;
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
