using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrection105Async : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Асинхронная печать коррекции 1.0.5";

    public async Task<SkkmConnector> PostPrintCheckCorrection105Async()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);

        connector.NewCheckCorrection(cashierName, CheckType.CorrectionSale, TaxSystem.Osn,
            CorrectionTypes.Самостоятельно, "Основание коррекции", DateTime.Today);
        connector.CorrectionSumTax20 = 16.67m;
        var line1 = connector.AddPosition("Товар по коррекции 1.05", 1, 100, TaxRate.Vat20);
        line1.Price = 100;
        connector.Cash = 100;
        await connector.PrintCheckCorrection105Async();
        if (connector.Ok)
            Console.WriteLine($"Чек коррекции поставлен в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
