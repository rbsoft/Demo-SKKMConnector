using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckReturnByFpAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Асинхронная печать чека возврата по ФП";

    public async Task<SkkmConnector> PostPrintCheckReturnByFpAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        connector.IsElectronically = false;
        connector.AdditionalAttribute = "1775661887";
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerEmail = "kuznicov@mail.ru";
        connector.CustomerVatin = "500100732259";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 73.18m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 73.18m;
        line1.DiscountSum = 0;
        line1.Department = 1;
        line1.TaxSum = 0;
        connector.Cash = 90;
        await connector.PrintCheckAsync();
        if (connector.Ok)
            Console.WriteLine($"Чек поставлен в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
