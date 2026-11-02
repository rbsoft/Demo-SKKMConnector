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
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Новый чек: коррекция прихода, СНО — общая:
        connector.NewCheck(CheckType.CorrectionSale, TaxSystem.Osn);

        // Данные коррекции:
        connector.CorrectionDataType = CorrectionTypes.OnOwn;
        connector.CorrectionDataDate = new DateTime(2026, 10, 11);
        connector.CorrectionDataNumber = "0";
        connector.CorrectionDataDescription = "Коррекция выручки";

        connector.AddPosition("Коррекция", 1, 120, 120, TaxRate.None,
            SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);

        connector.Cash = 120;

        // Ставим чек коррекции в очередь:
        if (await connector.PrintCheckCorrection120Async())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
