using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CloseShift : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Закрыть смену";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> PostCloseShift()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.CloseShift();

        if (connector.Ok)
            Console.WriteLine(
                "Смена закрыта (Z-отчёт).\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Количество чеков за смену: {connector.CheckNumberInShift}\n" +
                $"Количество ФД за смену: {connector.ShiftDocumentsCount}\n" +
                $"Номер последнего чека за смену: {connector.ShiftClosingCheckNumber}\n" +
                $"Остаток наличных: {connector.CashBalance}\n" +
                "--- Необнуляемые итоги (OverallTotals) ---\n" +
                $"Загружены из ФН: {connector.OverallIsDataLoaded}\n" +
                $"Остаток в ящике: {connector.OverallCashDrawerSum} (операций: {connector.OverallCashDrawerCount})\n" +
                $"Приход всего: {connector.OverallSalesSum} (чеков: {connector.OverallSalesCount})\n" +
                $"Возврат прихода всего: {connector.OverallSalesReturnSum} (чеков: {connector.OverallSalesReturnCount})\n" +
                $"Расход всего: {connector.OverallPurchasesSum} (чеков: {connector.OverallPurchasesCount})\n" +
                $"Возврат расхода всего: {connector.OverallPurchasesReturnSum} (чеков: {connector.OverallPurchasesReturnCount})\n" +
                $"Коррекции всего: {connector.OverallCorrectionSum} (кол-во: {connector.OverallCorrectionsNumber})\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Срок действия ФН: {connector.FnValidityDate}\n" +
                $"Остаток ресурса ФН в днях: {connector.FnDaysResources}\n" +
                $"Количество непереданных в ОФД документов: {connector.BacklogDocumentsCounter}\n" +
                $"Номер первого непереданного документа: {connector.BacklogDocumentFirstNumber}\n" +
                $"Дата и время первого непереданного документа: {connector.BacklogDocumentFirstDateTime}\n" +
                $"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}\n" +
                $"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}\n" +
                $"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}\n" +
                $"Превышено время ожидания ОФД: {connector.FnWarningsIsOfdTimeout}\n" +
                $"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}