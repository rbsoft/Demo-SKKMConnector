using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Штучный маркированный товар без отраслевого реквизита: КМ, GTIN, StampType — как в демо 1С.
/// </summary>
public class CheckSampleMarkedPiece : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Штучная маркировка";
    public const int SortOrder = 24;

    public async Task<SkkmConnector> PostCheckSampleMarkedPiece()
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

        var row = connector.AddPosition(
            "Пачка сигарет",
            1m,
            150m,
            150m,
            TaxRate.Vat0, // ставка НДС 0%
            SignCalculationObject.MarkedWithCode, // товар с маркировкой с кодом (33)
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки
        // Код марки в формате Base64
        row.Cis_Base64 = "MDEwNDYyOTMwODg3NzA0NDIxRHprY1dxUx04MDA1MTc3MDAwHTkzZEdWeg==";
        row.GTIN = "04629308877044"; // GTIN
        row.StampType = "05"; // тип маркировки

        connector.Cash = 150m; // сумма наличными

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
