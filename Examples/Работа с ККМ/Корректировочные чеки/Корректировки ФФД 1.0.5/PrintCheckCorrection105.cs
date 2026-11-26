using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection105 : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Печать чека коррекции 1.0.5";
    public const bool DocumentIdOptional = true;

    public async Task<SkkmConnector> PostPrintCheckCorrection105()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.CorrectionSale задаёт чеку тип операции «коррекция прихода».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.CorrectionSale, TaxSystem.Osn);

        // Id документа для повторной печати (необязательно, задаётся после NewCheck): если чек
        // с таким DocId уже есть, сервер новый не создаст, а напечатает его повторно.
        if (documentId.Length > 0)
            // Идентификатор документа (DocId) — изпрошлого вызова:
            connector.DocId = documentId;

        // Основание коррекции (теги 1173–1177 ФФД 1.0.5):
        connector.CorrectionDataType = CorrectionTypes.OnOwn; // самостоятельно (без предписания ФНС); ByPrescription — по предписанию
        connector.CorrectionDataDate = new DateTime(2026, 10, 11, 15, 30, 0); // дата совершения корректируемого расчёта
        connector.CorrectionDataNumber = "0"; // номер предписания налогового органа; «0» — без предписания
        connector.AdditionalAttribute = "2474536032";

        connector.Cash = 100; // сумма наличными (итог коррекции)

        // Сумма НДС по ставке 20%. Остальные ставки: SumTaxNone, SumTax0, SumTax5, SumTax105,
        // SumTax7, SumTax107, SumTax10, SumTax110, SumTax18, SumTax118, SumTax120, SumTax22, SumTax122.
        // Позиции в чек коррекции ФФД 1.0.5 не передаются.
        connector.SumTax20 = 16.67m;

        // Печатаем чек коррекции:
        if (await connector.PrintCheckCorrection105())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
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
