using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample39 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Электронный чек";

    public async Task<SkkmConnector> PostCheckSample39()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
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
        connector.IndustryIdentifierFoiv = "030";
        connector.IndustryAttributeDocumentDate = "21.11.2023";
        connector.IndustryAttributeDocumentNumber = "1944";
        connector.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";
        connector.UserAttributeName = "НомерЗаказа";
        connector.UserAttributeValue = "42";
        connector.OperationalAttributeDateTime = "03.07.2026";
        connector.OperationalAttributeOperationId = 1;
        connector.OperationalAttributeData = "Оплата по договору";
        var line1 = connector.AddPosition("Услуга", 1m, 250.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 250m;
        line1.TaxSum = 41.67m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 250.0m;
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
