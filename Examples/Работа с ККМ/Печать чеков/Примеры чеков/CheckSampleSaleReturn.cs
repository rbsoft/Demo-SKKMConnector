using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Возврат прихода (NewCheck тип 2) — как в демо 1С.
/// </summary>
public class CheckSampleSaleReturn : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Возврат прихода";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostCheckSampleSaleReturn()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.SaleReturn — возврат прихода.
        // TaxSystem.Osn — ОСН (общая).
        connector.NewCheck(CheckType.SaleReturn, TaxSystem.Osn);

        connector.AddPosition(
            "Вода 1л.",
            1m,
            50m,
            50m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки

        connector.Cash = 50m; // сумма наличными

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
