using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Приход с оплатой только авансом (зачёт предоплаты) — как в демо 1С.
/// </summary>
public class CheckSampleAdvance : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Аванс";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> PostCheckSampleAdvance()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale — приход.
        // TaxSystem.Osn — ОСН (общая).
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition(
            "Вода 1л.",
            1m,
            80m,
            80m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки

        connector.AdvancePayment = 80m; // сумма авансом (зачёт предоплаты)

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
