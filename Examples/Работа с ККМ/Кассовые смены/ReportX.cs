using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ReportX : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "X-отчёт";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> PostReportX()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.ReportX();

        if (connector.Ok)
        {
            Console.WriteLine(
                "X-отчёт сформирован.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Остаток наличных: {connector.CashBalance}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                "--- Итоги смены ---\n" +
                $"Остаток в ящике: {connector.ShiftTotalCashDrawerSum} (операций: {connector.ShiftTotalCashDrawerCount})\n" +
                $"Внесения: {connector.ShiftTotalIncomeSum} (кол-во: {connector.ShiftTotalIncomeCount})\n" +
                $"Выемки: {connector.ShiftTotalOutcomeSum} (кол-во: {connector.ShiftTotalOutcomeCount})\n" +
                $"Приход: {connector.ShiftTotalSalesSum} (чеков: {connector.ShiftTotalSalesCount})\n" +
                $"Возврат прихода: {connector.ShiftTotalSalesReturnSum} (чеков: {connector.ShiftTotalSalesReturnCount})\n" +
                $"Расход: {connector.ShiftTotalPurchasesSum} (чеков: {connector.ShiftTotalPurchasesCount})\n" +
                $"Возврат расхода: {connector.ShiftTotalPurchasesReturnSum} (чеков: {connector.ShiftTotalPurchasesReturnCount})\n" +
                $"Коррекция прихода: {connector.ShiftTotalSalesCorrectionSum} (чеков: {connector.ShiftTotalSalesCorrectionCount})\n" +
                $"Коррекция возврата прихода: {connector.ShiftTotalSalesReturnCorrectionSum} (чеков: {connector.ShiftTotalSalesReturnCorrectionCount})\n" +
                $"Коррекция расхода: {connector.ShiftTotalPurchasesCorrectionSum} (чеков: {connector.ShiftTotalPurchasesCorrectionCount})\n" +
                $"Коррекция возврата расхода: {connector.ShiftTotalPurchasesReturnCorrectionSum} (чеков: {connector.ShiftTotalPurchasesReturnCorrectionCount})\n" +
                $"Приход за всё время: {connector.OverallSalesSum} (чеков: {connector.OverallSalesCount})\n" +
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
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}