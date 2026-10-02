using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportX : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Результат X-отчёта";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 10;

    public async Task<SkkmConnector> GetGetReportX()
    {
        connector.DeviceName = deviceName;
        await connector.GetReportX(documentId);
        if (connector.Ok)
            Console.WriteLine(
                "X-отчёт получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Непереданных в ОФД: {connector.BacklogDocumentsCounter}\n" +
                "--- Итоги смены ---\n" +
                $"Счётчики прочитаны из ФН: {connector.ShiftTotalIsCountersReaded}\n" +
                $"Номер смены (итоги): {connector.ShiftTotalShiftNumber}\n" +
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
                $"Коррекции: {connector.ShiftTotalCorrectionSum} (кол-во: {connector.ShiftTotalCorrectionsNumber})\n" +
                "--- Необнуляемые итоги ---\n" +
                $"Загружены из ФН: {connector.OverallIsDataLoaded}\n" +
                $"Приход всего: {connector.OverallSalesSum} (чеков: {connector.OverallSalesCount})\n" +
                $"Возврат прихода всего: {connector.OverallSalesReturnSum} (чеков: {connector.OverallSalesReturnCount})\n" +
                $"Расход всего: {connector.OverallPurchasesSum} (чеков: {connector.OverallPurchasesCount})\n" +
                $"Возврат расхода всего: {connector.OverallPurchasesReturnSum} (чеков: {connector.OverallPurchasesReturnCount})\n" +
                $"Коррекции всего: {connector.OverallCorrectionSum} (кол-во: {connector.OverallCorrectionsNumber})\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"СНО: {connector.CheckTaxType}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                $"Адрес расчётов: {connector.CheckSaleAddress}\n" +
                $"Место расчётов: {connector.CheckSaleLocation}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"ОФД: {connector.OfdName}, ИНН ОФД: {connector.OfdVatin}\n" +
                $"Устройство: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, ФФД {connector.DeviceFfdVersion})");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
