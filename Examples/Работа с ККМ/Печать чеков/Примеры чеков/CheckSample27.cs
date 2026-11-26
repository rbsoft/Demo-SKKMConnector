using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// СНО задаётся на весь чек — разные системы нельзя смешать в одном запросе.
/// Этот пример: УСН доход минус расход.
/// </summary>
public class CheckSample27 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "УСН Д-Р";
    public const int SortOrder = 12;

    public async Task<SkkmConnector> PostCheckSample27()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale задаёт чеку тип операции «приход».
        // TaxSystem.UsnDr задаёт чеку систему налогообложения УСН доход минус расход.
        connector.NewCheck(CheckType.Sale, TaxSystem.UsnDr);
        // Добавляем позицию в чек (СНО уже задана в NewCheck — УСН доход минус расход).
        var line1 = connector.AddPosition("Вода 1л.",
            1m,
            20m,
            20m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // способ расчёта — полный расчёт
            MeasureOfQuantity.Piece); // мера количества — штуки
        line1.TaxSum = 0m; // сумма НДС по строке
        connector.Cash = 20m; // сумма наличными
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
