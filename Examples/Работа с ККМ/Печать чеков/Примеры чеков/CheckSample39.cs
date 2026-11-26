using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Онлайн-оплата: электронный чек, расчёт в сети Интернет, покупатель,
/// отраслевые / пользовательские / операционные реквизиты.
/// </summary>
public class CheckSample39 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Онлайн оплата";
    public const int SortOrder = 30;

    public async Task<SkkmConnector> PostCheckSample39()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale задаёт чеку тип операции «приход».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        // Шапка и подвал чека
        connector.TextBefore = "Текст до товаров";
        connector.TextAfter = "Текст после товаров";

        // Добавляем позицию в чек
        connector.AddPosition(
            "Услуга",
            1m,
            250m,
            250m,
            TaxRate.Vat20, // ставка НДС 20%
            SignCalculationObject.Service, // услуга
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки

        // Электронный чек и расчёт в сети Интернет
        connector.IsElectronically = true;
        connector.IsOperationOnline = true;

        // Данные о продаже
        connector.SenderEmail = "ivanov@mail.ru";
        connector.SaleAddress = "г. Москва, ул. Ленина, д. 1";
        connector.SaleLocation = "https://shop.ru";

        // Часовой пояс (2 — МСК)
        connector.TimeZone = CheckTimeZone.Msk;

        // Доп. реквизит чека
        connector.AdditionalAttribute = "6702704322";

        // Данные покупателя
        connector.CustomerInfo = "Иванов И. И.";
        connector.CustomerVatin = "500100732259";
        connector.CustomerEmail = "ivanov@mail.ru";
        connector.CustomerPhone = "+79001234567";
        connector.CustomerDateOfBirth = "01.01.1990";
        connector.CustomerCitizenship = "643";
        connector.CustomerDocumentTypeCode = "21";
        connector.CustomerDocumentData = "4509 123456";
        connector.CustomerAddress = "г. Москва, ул. Ленина, д. 1";

        // Дополнительный реквизит пользователя (тег 1086)
        connector.UserAttributeName = "НомерЗаказа";
        connector.UserAttributeValue = "42";

        // Операционный реквизит чека (тег 1270)
        connector.OperationalAttributeDateTime = "03.07.2026";
        connector.OperationalAttributeOperationID = 1;
        connector.OperationalAttributeOperationData = "Оплата по договору";

        // Отраслевой реквизит чека (теги 1262–1265)
        connector.IndustryAttributeIdentifierFoiv = "030";
        connector.IndustryAttributeDocumentDate = "21.11.2023";
        connector.IndustryAttributeDocumentNumber = "1944";
        connector.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        connector.Cash = 250m; // сумма наличными

        if (await connector.PrintCheck())
        {

            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
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
