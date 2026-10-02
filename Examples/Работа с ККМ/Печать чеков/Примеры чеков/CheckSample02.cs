using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample02 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Подакцизный товар";

    public async Task<SkkmConnector> PostCheckSample02()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Usn);
        connector.NewCheck(CheckType.Sale);
        
        connector.IsElectronically = true;
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "kuznicov@mail.ru";
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1m, 10m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.ExcisableGoods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 10m;
        line1.Department = 0;
        line1.TaxSum = 0m;
        line1.ExciseAmount = 1m;
        var line2 = connector.AddPosition("Пиво", 3m, 31.50m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.ExcisableGoods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 10.50m;
        line2.Department = 1;
        line2.TaxSum = 0m;
        connector.Cash = 100.01m;

        await connector.PrintCheck();
        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
