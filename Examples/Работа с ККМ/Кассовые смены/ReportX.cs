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
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
                $"Количество документов за смену: {connector.ShiftDocumentsCount}\n" +
                $"Остаток наличных: {connector.CashBalance}\n" +
                "--- Состояние ФН ---\n" +
                $"Срок действия ФН: {connector.FnValidityDate}\n" +
                $"Остаток ресурса ФН (дней): {connector.FnDaysResources}\n" +
                $"Непереданных в ОФД документов: {connector.BacklogDocumentsCounter}\n" +
                $"Первый непереданный: №{connector.BacklogDocumentFirstNumber} от {connector.BacklogDocumentFirstDateTime}\n" +
                $"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}\n" +
                $"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}\n" +
                $"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}\n" +
                $"Превышено время ожидания ОФД: {connector.FnWarningsIsOfdTimeout}\n" +
                $"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}\n" +
                "--- Реквизиты ---\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"ОФД: {connector.OfdName}, ИНН ОФД: {connector.OfdVatin}\n" +
                $"Адрес расчётов: {connector.CheckSaleAddress}\n" +
                $"Место расчётов: {connector.CheckSaleLocation}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                "--- Устройство ---\n" +
                $"Касса: {connector.CheckDeviceName}, терминал: {connector.CheckTerminalId}\n" +
                $"Модель: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, конфигурация {connector.DeviceConfigurationVersion})\n" +
                $"Версия ФФД ККТ: {connector.DeviceFfdVersion}, ФФД ФН: {connector.FnFfdVersion}, тип: {connector.DeviceClass}, часовая зона: {connector.DeviceTimeZone}\n" +
                $"Фискальный режим: {connector.IsFiscal}, ширина строки: {connector.LineLength} симв. / {connector.LineLengthPixels} пикс.\n" +
                $"Версия сервера ККМ: {connector.CheckServerVersion}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}