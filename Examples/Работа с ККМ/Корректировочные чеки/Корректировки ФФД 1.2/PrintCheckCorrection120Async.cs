using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection120Async : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Асинхронная печать коррекции 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrection120Async()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.CorrectionSale задаёт чеку тип операции «коррекция прихода».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.CorrectionSale, TaxSystem.Osn);

        // Основание коррекции для ФФД 1.2:
        connector.CorrectionDataType = CorrectionTypes.OnOwn; // самостоятельно (без предписания ФНС); ByPrescription — по предписанию
        connector.CorrectionDataDate = new DateTime(2026, 10, 11); // дата корректируемого расчёта
        connector.CorrectionDataNumber = "0"; // номер предписания; «0» — без предписания ФНС
        connector.CorrectionDataDescription = "Коррекция выручки"; // содержание основания

        connector.AddPosition("Коррекция", 1, 120, 120, TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки

        connector.Cash = 120; // сумма наличными
        if (await connector.PrintCheckCorrection120Async())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
