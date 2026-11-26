using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Частичное выбытие штучного маркированного товара (пачка, ФФД 1.2, тег 1291):
/// количество предмета расчёта = 1 (одна потребительская упаковка с КМ),
/// дробь числитель/знаменатель = сколько единиц из упаковки продаём / сколько единиц в упаковке.
/// Пример: 1/2 — половина упаковки. Числитель строго меньше знаменателя, оба ≠ 0.
/// </summary>
public class CheckSampleFractional : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Дробная маркировка";
    public const int SortOrder = 26;

    public async Task<SkkmConnector> PostCheckSampleFractional()
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

        // Штучный маркированный товар: Quantity = 1 (упаковка с одним КМ),
        // частичная реализация — через FractionalQuantity (тег 1291), не через Quantity < 1.
        var row = connector.AddPosition(
            "Пачка сигарет",
            1m, // количество предмета расчёта — одна маркированная упаковка
            80m,
            80m,
            TaxRate.Vat20, // ставка НДС 20%
            SignCalculationObject.MarkedWithCode, // признак предмета — товар с маркировкой с кодом (33)
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // мера — штуки
        // Дробное количество маркированного товара (тег 1291): 1 из 2 единиц в упаковке
        row.FractionalQuantityNumerator = 1; // числитель (тег 1293) — сколько продаём
        row.FractionalQuantityDenominator = 2; // знаменатель (тег 1294) — единиц в упаковке с этим КМ
        // Код марки в формате Base64
        row.Cis_Base64 = "MDEwNDYyOTMwODg3NzA0NDIxRHprY1l0Mh04MDA1MDkwMDAwHTkzZEdWeg==";

        connector.Cash = 80m; // сумма наличными

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
