using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Измеряемый маркированный товар: сыр по весу (кг), КМ, без дробного количества 1291.
/// </summary>
public class CheckSampleWeighedMarked : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Сыр по весу";
    public const int SortOrder = 25;

    public async Task<SkkmConnector> PostCheckSampleWeighedMarked()
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

        // Весовой маркированный товар: Quantity в кг, мера — килограмм (тег 2108 = 11).
        // FractionalQuantity не нужен — частичное выбытие штучной упаковки здесь не применяется.
        var row = connector.AddPosition(
            "Сыр",
            0.353m, // количество — 0,353 кг (тег 1023)
            200m,
            70.6m,
            TaxRate.Vat0, // ставка НДС 0%
            SignCalculationObject.MarkedWithCode, // товар с маркировкой с кодом (33)
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Kilogram); // мера — килограмм
        // Код марки в формате Base64
        row.Cis_Base64 = "MDEwNDYxMDEzNjI4MDU3MTIxJTc5OERNJR04MDA1MDkwMDAwHTkzZEdWeg==";

        connector.Cash = 70.6m; // сумма наличными

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
