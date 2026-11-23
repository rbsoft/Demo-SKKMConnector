using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Возврат чека покупки (отмена выплаты по покупке).
/// </summary>
public class CheckSample06 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Возврат расхода";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> PostCheckSample06()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.PurchaseReturn задаёт чеку тип операции «возврат расхода».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.PurchaseReturn, TaxSystem.Osn);
        // Добавляем позицию в чек (отмена выплаты по чеку «покупка»):
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
