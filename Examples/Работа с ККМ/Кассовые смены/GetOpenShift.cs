using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOpenShift : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Результат открытия смены";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetOpenShiftById()
    {
        connector.DeviceName = deviceName;
        await connector.GetOpenShift(documentId);

        if (connector.Ok)
            Console.WriteLine(
                "Документ открытия смены получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Номер ФД за смену: {connector.CheckDocNumberInShift}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
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
                "--- Статус обмена с ОФД ---\n" +
                $"Соединение с ОФД установлено: {connector.OfdStatusIsConnectedOFD}\n" +
                $"Обмен завершён: {connector.OfdStatusIsCompleted}\n" +
                $"Есть документы для отправки: {connector.OfdStatusIsExistDocsToSend}\n" +
                $"Непереданных документов: {connector.OfdStatusDocumentsCount}\n" +
                $"Первый непереданный: №{connector.OfdStatusFirstDocumentNumber} от {connector.OfdStatusFirstDocumentDate:dd.MM.yyyy HH:mm}\n" +
                $"Устройство: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, ФФД {connector.DeviceFfdVersion})");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
