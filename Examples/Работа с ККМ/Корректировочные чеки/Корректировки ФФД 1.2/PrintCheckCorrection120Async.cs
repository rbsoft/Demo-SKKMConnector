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

        connector.NewCheckCorrection(cashierName, CheckType.CorrectionSale, TaxSystem.Osn,
            CorrectionTypes.Самостоятельно, "Коррекция расчёта", DateTime.Today);
        var line1 = connector.AddPosition("Товар по коррекции", 1, 100, TaxRate.Vat20);
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
