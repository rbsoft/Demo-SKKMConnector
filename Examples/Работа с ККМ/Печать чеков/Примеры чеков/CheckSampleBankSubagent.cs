using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Банковский платёжный субагент в заголовке (AgentSign = 1) + оператор перевода — как в демо 1С.
/// </summary>
public class CheckSampleBankSubagent : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Банковский субагент";
    public const int SortOrder = 17;

    public async Task<SkkmConnector> PostCheckSampleBankSubagent()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        // 1 — банковский платёжный субагент; признак на весь чек
        connector.AgentSign = AgentType.BankPaymentSubagent;
        connector.VendorDataName = "ООО Ромашка";
        connector.VendorDataPhones = new[] { "+79001234567" };
        connector.VendorDataVatin = "7701234560";
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = new[] { "+79001234567" };
        connector.ReceivePaymentsOperatorPhone = new[] { "+79001234567" };
        connector.MoneyTransferOperatorPhone = new[] { "+79001234567" };
        connector.MoneyTransferOperatorName = "ООО Рога и копыта";
        connector.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        connector.MoneyTransferOperatorVatin = "7701234560";

        connector.AddPosition("Вода 1л.", 1m, 61m, 61m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.Cash = 61m;

        if (await connector.PrintCheck())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
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
