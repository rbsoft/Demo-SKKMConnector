using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheck : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Печать чека (базовый)";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostPrintCheck()
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

        // Добавляем позицию в чек: товар, полная оплата, 1 шт. × 150 ₽ = 150 ₽.
        connector.AddPosition("Кофе американо", 1, 150, 150, TaxRate.Vat20,
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment); // способ расчёта — полный расчёт
        connector.Cash = 150; // сумма наличными
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
