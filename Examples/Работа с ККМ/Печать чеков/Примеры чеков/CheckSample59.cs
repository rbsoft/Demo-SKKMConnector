using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Маркированный товар с отраслевым реквизитом на позиции (теги 1262–1265).
/// </summary>
public class CheckSample59 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Маркировка + отраслевой";
    public const int SortOrder = 27;

    public async Task<SkkmConnector> PostCheckSample59()
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

        // Пачка сигарет + отраслевой реквизит на позиции (не на чеке).
        var row = connector.AddPosition(
            "Пачка сигарет",
            1m,
            150m,
            150m,
            TaxRate.Vat0, // ставка НДС 0%
            SignCalculationObject.MarkedWithCode, // признак предмета — товар с маркировкой с кодом (33)
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // мера — штуки
        // Код марки в формате Base64
        row.Cis_Base64 = "MDQ2MDE2NTMwMzU4MjlIO2RWKWJGQUNWVWRHVno=";
        // Идентификатор ФОИВ (тег 1262)
        row.IndustryAttributeIdentifierFoiv = "030";
        // Дата документа-основания
        row.IndustryAttributeDocumentDate = "21.11.2023";
        // Номер документа-основания
        row.IndustryAttributeDocumentNumber = "1944";
        // Значение отраслевого реквизита (тег 1265)
        row.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

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
