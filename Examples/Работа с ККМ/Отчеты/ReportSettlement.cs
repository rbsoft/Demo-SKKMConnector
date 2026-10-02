using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportSettlement : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Отчёт расчётов";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostReportSettlement()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.ReportSettlement();

        if (connector.Ok)
        {
            Console.WriteLine(
                "Отчёт о состоянии расчётов сформирован.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Количество непереданных в ОФД документов: {connector.BacklogDocumentsCounter}\n" +
                $"Номер первого непереданного документа: {connector.BacklogDocumentFirstNumber}\n" +
                $"Дата и время первого непереданного документа: {connector.BacklogDocumentFirstDateTime}\n" +
                $"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}\n" +
                $"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}\n" +
                $"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}\n" +
                $"Превышено время ожидания ОФД: {connector.FnWarningsIsOfdTimeout}\n" +
                $"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}