using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection120Async : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Асинхронная печать коррекции 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrection120Async()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.CorrectionSale);
        connector.CorrectionType = CorrectionTypes.Самостоятельно;
        connector.CorrectionDescription = "Коррекция расчёта";
        connector.CorrectionDate = DateTime.Today;
        var line1 = connector.AddPosition("Товар по коррекции", 1, 100, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 100;
        connector.Cash = 100;
        await connector.PrintCheckCorrection120Async();
        if (connector.Ok)
            Console.WriteLine($"Чек коррекции поставлен в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
