using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection120 : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Коррекция прихода 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrection120()
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

        // Основание коррекции для ФФД 1.2 (позиции в чеке обязательны):
        connector.CorrectionDataType = CorrectionTypes.OnOwn; // самостоятельно (без предписания ФНС); ByPrescription — по предписанию
        connector.CorrectionDataDate = new DateTime(2026, 10, 11); // дата корректируемого расчёта
        connector.CorrectionDataNumber = "0"; // номер предписания
        connector.CorrectionDataDescription = "Коррекция выручки"; // содержание основания (текст для ФН)

        // Позиция чека коррекции — передаётся в ОФД вместе с суммой оплаты:
        connector.AddPosition("Коррекция", 1, 120, 120, TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки

        connector.Cash = 120; // сумма наличными

        // Печатаем чек коррекции:
        if (await connector.PrintCheckCorrection120())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа (ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
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
