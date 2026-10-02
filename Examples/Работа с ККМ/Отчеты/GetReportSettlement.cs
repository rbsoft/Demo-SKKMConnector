using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportSettlement : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Отчёт расчётов по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetReportSettlementById()
    {
        connector.DeviceName = deviceName;
        await connector.GetReportSettlement(documentId);

        if (connector.Ok)
            Console.WriteLine(
                "Отчёт о состоянии расчётов получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Непереданных в ОФД: {connector.BacklogDocumentsCounter}\n" +
                $"Первый непереданный: №{connector.BacklogDocumentFirstNumber} от {connector.BacklogDocumentFirstDateTime:dd.MM.yyyy HH:mm}\n" +
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
