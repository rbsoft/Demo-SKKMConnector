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
        connector.SetCashier(cashierName, cashierVatin);
        connector.NewCheck(CheckType.Sale);
        connector.SetTaxType(TaxSystem.Envd);
        connector.IsElectronically = false;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.Cash = 30;
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 30, TaxRate.Vat0, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 30;
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        var line2 = connector.AddPosition("Бутылка с крышкой (пустая) 4л.", 1, 30, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 30;
        line2.DiscountSum = 0;
        line2.Department = 1;
        line2.TaxSum = 5;
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        await connector.PrintCheckAsync();
        if (connector.Ok)
            Console.WriteLine($"Чек поставлен в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
