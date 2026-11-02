using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample39 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Электронный чек";

    public async Task<SkkmConnector> PostCheckSample39()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.TimeZone = CheckTimeZone.Msk;
        connector.IsElectronically = true;
        connector.IsOperationOnline = true;
        connector.SenderEmail = "ivanov@mail.ru";
        connector.SaleAddress = "г. Москва, ул. Ленина, д. 1";
        connector.SaleLocation = "https://shop.ru";
        connector.AdditionalAttribute = "6702704322";
        connector.TextBefore = "Текст до товаров";
        connector.TextAfter = "Текст после товаров";
        connector.CustomerInfo = "Иванов И. И.";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "ivanov@mail.ru";
        connector.CustomerPhone = "+79001234567";
        connector.CustomerDateOfBirth = "01.01.1990";
        connector.CustomerCitizenship = "643";
        connector.CustomerDocumentTypeCode = "21";
        connector.CustomerDocumentData = "4509 123456";
        connector.CustomerAddress = "г. Москва, ул. Ленина, д. 1";
        connector.IndustryAttributeIdentifierFoiv = "030";
        connector.IndustryAttributeDocumentDate = "21.11.2023";
        connector.IndustryAttributeDocumentNumber = "1944";
        connector.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";
        connector.UserAttributeName = "НомерЗаказа";
        connector.UserAttributeValue = "42";
        connector.OperationalAttributeDateTime = "03.07.2026";
        connector.OperationalAttributeOperationID = 1;
        connector.OperationalAttributeOperationData = "Оплата по договору";
        var line1 = connector.AddPosition("Услуга", 1m, 250m, 250.0m, TaxRate.Vat20, SignCalculationObject.Service, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 41.67m;
        connector.Cash = 250.0m;
        if (await connector.PrintCheck())
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
