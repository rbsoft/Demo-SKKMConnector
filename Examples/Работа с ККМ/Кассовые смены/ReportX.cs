using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportX : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "X-отчёт";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> PostReportX()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        if (await connector.ReportX())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Количество кассовых чеков за смену: {connector.NumberOfChecks}");
            Console.WriteLine($"Количество общее ФД за смену: {connector.NumberOfDocuments}");
            Console.WriteLine($"Срок действия ключей фискального признака в днях: {connector.ResourcesFn}");
            Console.WriteLine($"Номер последнего фискального документа: {connector.CheckNumber}");
            Console.WriteLine($"Номер последнего чека за смену: {connector.ShiftClosingCheckNumber}");
            Console.WriteLine($"Дата и время формирования фискального документа: {connector.DateTime}");
            Console.WriteLine($"Состояние смены: {connector.ShiftState}");
            Console.WriteLine($"Остаток наличных денежных средств в денежном ящике: {connector.CashBalance}");
            Console.WriteLine($"Срок действия ФН: {connector.FnValidityDate}");
            Console.WriteLine($"Количество непереданных документов: {connector.BacklogDocumentsCounter}");
            Console.WriteLine($"Номер первого непереданного документа: {connector.BacklogDocumentFirstNumber}");
            Console.WriteLine($"Дата и время первого из непереданных документов: {connector.BacklogDocumentFirstDateTime}");
            Console.WriteLine($"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}");
            Console.WriteLine($"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}");
            Console.WriteLine($"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}");
            Console.WriteLine($"Превышено время ожидания ответа от ОФД: {connector.FnWarningsIsOfdTimeout}");
            Console.WriteLine($"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}