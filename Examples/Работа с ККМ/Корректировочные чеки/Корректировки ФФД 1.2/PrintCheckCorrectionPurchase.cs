using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCorrectionPurchase : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Коррекция расхода 1.2";

    public async Task<SkkmConnector> PostPrintCheckCorrectionPurchase()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.CorrectionPurchase, TaxSystem.Osn);
        connector.CorrectionDataType = CorrectionTypes.OnOwn;
        connector.CorrectionDataDescription = "Коррекция расхода";
        connector.CorrectionDataDate = DateTime.Today;
        var line1 = connector.AddPosition("Товар по коррекции расхода", 1, 100, 100, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment);
        connector.Cash = 100;
        if (await connector.PrintCheckCorrection120())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
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
