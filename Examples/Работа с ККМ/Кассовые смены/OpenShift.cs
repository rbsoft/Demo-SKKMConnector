using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OpenShift : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Открыть смену";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostOpenShift()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.OpenShift();

        if (connector.Ok)
            Console.WriteLine(
                "Смена открыта.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер чека за смену: {connector.CheckNumberInShift}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
                $"Остаток наличных: {connector.CashBalance}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ: {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Срок действия ФН: {connector.FnValidityDate}\n" +
                $"Остаток ресурса ФН в днях: {connector.FnDaysResources}\n" +
                $"Количество непереданных в ОФД документов: {connector.BacklogDocumentsCounter}\n" +
                $"Номер первого непереданного документа: {connector.BacklogDocumentFirstNumber}\n" +
                $"Дата и время первого непереданного документа: {connector.BacklogDocumentFirstDateTime}\n" +
                $"Количество документов в смене: {connector.ShiftDocumentsCount}\n" +
                $"Номер чека закрытия смены: {connector.ShiftClosingCheckNumber}\n" +
                $"Модель ККТ: {connector.DeviceModel}\n" +
                $"Заводской номер ККТ: {connector.DeviceSerialNumber}\n" +
                $"Версия прошивки: {connector.DeviceFirmwareVersion}\n" +
                $"Версия конфигурации прошивки: {connector.DeviceConfigurationVersion}\n" +
                $"Версия ФФД: {connector.DeviceFfdVersion}\n" +
                $"Версия ФФД ФН: {connector.FnFfdVersion}\n" +
                $"Тип устройства: {connector.DeviceClass}\n" +
                $"Часовая зона устройства: {connector.DeviceTimeZone}\n" +
                $"Фискальный режим: {connector.IsFiscal}\n" +
                $"Ширина чековой ленты (символы): {connector.LineLength}\n" +
                $"Ширина чековой ленты (пиксели): {connector.LineLengthPixels}\n" +
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