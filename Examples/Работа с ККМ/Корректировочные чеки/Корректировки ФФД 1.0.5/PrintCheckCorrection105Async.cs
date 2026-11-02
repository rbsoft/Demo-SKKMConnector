using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection105Async : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Асинхронная печать коррекции 1.0.5";
    public const bool DocumentIdOptional = true;

    public async Task<SkkmConnector> PostPrintCheckCorrection105Async()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Новый чек: коррекция прихода, СНО — общая:
        connector.NewCheck(CheckType.CorrectionSale, TaxSystem.Osn);

        // Id документа для повторной печати (необязательно, задаётся после NewCheck): если чек
        // с таким DocId уже есть, сервер новый не создаст, а напечатает его повторно.
        if (documentId.Length > 0)
            connector.DocId = documentId;

        // Данные коррекции:
        connector.CorrectionDataType = CorrectionTypes.OnOwn;
        connector.CorrectionDataDate = new DateTime(2026, 10, 11, 15, 30, 0);
        connector.CorrectionDataNumber = "0";
        // Дополнительный реквизит чека (ФП корректируемого чека):
        connector.AdditionalAttribute = "2474536032";

        connector.Cash = 100;

        // Сумма НДС по ставке 20%. Остальные ставки: SumTaxNone, SumTax0, SumTax5, SumTax105,
        // SumTax7, SumTax107, SumTax10, SumTax110, SumTax18, SumTax118, SumTax120, SumTax22, SumTax122.
        // Позиции в чек коррекции ФФД 1.0.5 не передаются.
        connector.SumTax20 = 16.67m;

        // Ставим чек коррекции в очередь:
        if (await connector.PrintCheckCorrection105Async())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
