using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Асинхронная печать чека";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostPrintCheckAsync()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Envd);
        connector.IsElectronically = false;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.Cash = 30;
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 30, 30, TaxRate.Vat0, SignCalculationObject.Service, SignMethodCalculation.FullPayment);
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        var line2 = connector.AddPosition("Бутылка с крышкой (пустая) 4л.", 1, 30, 30, TaxRate.Vat20, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line2.DiscountSum = 0;
        line2.Department = 1;
        line2.TaxSum = 5;
        if (await connector.PrintCheckAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
