using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Одна позиция и все виды оплаты в одном чеке (сумма 100 ₽ по 20 ₽ на каждый вид).
/// </summary>
public class CheckSample11 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Смешанная оплата";
    public const int SortOrder = 9;

    public async Task<SkkmConnector> PostCheckSample11()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale задаёт чеку тип операции «приход».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        // Добавляем одну позицию — на неё ниже разнесены все виды оплаты.
        var line1 = connector.AddPosition("Вода 1л.",
            1m,
            100m,
            100.0m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки
        line1.TaxSum = 0m; // сумма НДС по строке
        // Виды оплаты в одном чеке (сумма всех = сумме чека 100 ₽):
        connector.Cash = 20m; // наличными
        connector.ElectronicPayment = 20m; // безналичными
        connector.AdvancePayment = 20m; // предоплатой (зачёт аванса)
        connector.Credit = 20m; // постоплатой (в кредит)
        connector.CashProvision = 20m; // встречным предоставлением
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
