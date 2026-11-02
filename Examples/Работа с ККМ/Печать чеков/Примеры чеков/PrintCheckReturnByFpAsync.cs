using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturnByFpAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Асинхронная печать чека возврата по ФП";

    public async Task<SkkmConnector> PostPrintCheckReturnByFpAsync()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.IsElectronically = false;
        connector.AdditionalAttribute = "1775661887";
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.CustomerVatin = "500100732259";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 73.18m, 73.18m, TaxRate.Vat20, SignCalculationObject.Service, SignMethodCalculation.FullPayment);
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        connector.Cash = 90;
        if (await connector.PrintCheckAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
