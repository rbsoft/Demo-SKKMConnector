using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Чек «покупка» (выплата покупателю), а не продажа.
/// </summary>
public class CheckSample05 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Расход";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostCheckSample05()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Purchase задаёт чеку тип операции «расход».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.Purchase, TaxSystem.Osn);
        // Добавляем позицию в чек (в чеке «покупка» это выплата покупателю, не продажа):
        var line1 = connector.AddPosition("Вода 1л.",
            1m,
            50m,
            50.0m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // способ расчёта — полный расчёт
            MeasureOfQuantity.Piece); // мера — штуки
        line1.TaxSum = 0m; // сумма НДС по строке
        connector.Cash = 50; // сумма наличными
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
