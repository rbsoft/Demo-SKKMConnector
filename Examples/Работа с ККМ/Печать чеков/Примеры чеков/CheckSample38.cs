using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Приход (безнал) — одна транзакция AddElectronicPaymentInfo
/// </summary>
public class CheckSample38 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Безнал";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> PostCheckSample38()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition(
            "Вода 1л.",
            1m,
            80m,
            80m,
            TaxRate.None,
            SignCalculationObject.Goods,
            SignMethodCalculation.FullPayment,
            MeasureOfQuantity.Piece);

        // Сумма, признак способа (0 — предоплата 100%), RRN, доп. сведения — как в 1С
        connector.AddElectronicPaymentInfo(
            80m,
            ElectronicPaymentMethod.FullPrepayment,
            "RRN=123456789012",
            "Терминал №1, карта *1234");

        connector.ElectronicPayment = 80m; // сумма безнал

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
