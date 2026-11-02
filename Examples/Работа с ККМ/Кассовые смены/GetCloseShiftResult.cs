using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCloseShiftResult : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Результат закрытия смены";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 6;

    public async Task<SkkmConnector> GetGetCloseShift()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем результат закрытия смены по ИД документа:
        if (await connector.GetCloseShiftResult())
        {
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер фискального документа: {connector.CheckNumber}");
            Console.WriteLine($"Номер фискального документа за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Фискальный признак документа: {connector.FiscalSign}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Время регистрации операции по часам ККМ: {connector.FiscalDatetime}");
            Console.WriteLine($"Имя кассира: {connector.CashierName}");
            Console.WriteLine($"ИНН кассира: {connector.CashierVatin}");
            Console.WriteLine($"Адрес проведения расчетов: {connector.FnSaleAddress}");
            Console.WriteLine($"Место проведения расчетов: {connector.FnSaleLocation}");
            Console.WriteLine($"Версия ФФД: {connector.FnFFDVersion}");
            Console.WriteLine($"Тип чека: {connector.TaskType}");
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Дата создания документа: {connector.DateTime}");
            Console.WriteLine($"Идентификатор терминала, с которго пришел документ: {connector.TerminalId}");
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Пул, который назначен чеку: {connector.PoolId}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Описания результата: {connector.ResultDescription}");
            Console.WriteLine($"Признак удачного завершения обработки: {connector.IsProcessed}");
            Console.WriteLine($"Версия Сервера ККМ: {connector.ServerVersion}");
            Console.WriteLine($"Версия ФФД: {connector.DeviceFfdVersion}");
            Console.WriteLine($"Версия ФФД ФН: {connector.DeviceFnFfdVersion}");
            Console.WriteLine($"Часовая зона: {connector.DeviceTimeZone}");
            foreach (var license in connector.KktLicenses)
            {
                Console.WriteLine($"Наименование лицензии: {license.Name}");
                Console.WriteLine($"Номер лицензии: {license.Number}");
                Console.WriteLine($"Дата начала действия лицензии: {license.ValidFrom}");
                Console.WriteLine($"Дата окончания действия лицензии: {license.ValidUntil}");
                Console.WriteLine($"Максимально поддерживаемая конфигурация: {license.UnitVersion}");
                Console.WriteLine($"Описание последствий окончания лицензии: {license.Description}");
                Console.WriteLine($"Лицензия действует для текущей прошивки: {license.IsActive}");
            }
            Console.WriteLine($"Фискальный режим: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Ширина чековой ленты: {connector.DeviceLineLength}");
            Console.WriteLine($"Ширина чековой ленты в пикселях: {connector.DeviceLineLengthPixels}");
            Console.WriteLine($"Тип устройства: {connector.DeviceClass}");
            Console.WriteLine($"Название модели ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Версия прошивки: {connector.DeviceFirmwareVersion}");
            Console.WriteLine($"Версия конфигурации прошивки устройства: {connector.DeviceConfigurationVersion}");
            Console.WriteLine($"Организация: {connector.OrganizationInfo}");
            Console.WriteLine($"Серийний номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Адрес ФНС: {connector.FnFnsUrl}");
            Console.WriteLine($"Имя ОФД: {connector.FnOfdName}");
            Console.WriteLine($"ИНН ОФД: {connector.FnOfdVatin}");
            Console.WriteLine($"Количество кассовых чеков за смену: {connector.NumberOfChecks}");
            Console.WriteLine($"Количество общее ФД за смену: {connector.NumberOfDocuments}");
            Console.WriteLine($"Количество непереданных документов: {connector.BacklogDocumentsCounter}");
            Console.WriteLine($"Номер первого непереданного документа: {connector.BacklogDocumentFirstNumber}");
            Console.WriteLine($"Дата и время первого из непереданных документов: {connector.BacklogDocumentFirstDateTime}");
            Console.WriteLine($"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}");
            Console.WriteLine($"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}");
            Console.WriteLine($"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}");
            Console.WriteLine($"Превышено время ожидания ответа от ОФД: {connector.FnWarningsIsOfdTimeout}");
            Console.WriteLine($"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}");
            Console.WriteLine($"Срок действия ключей фискального признака в днях: {connector.ResourcesFn}");
            Console.WriteLine($"Номер открытой смены/Номер закрытой смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер последнего фискального документа: {connector.LastCheckNumber}");
            Console.WriteLine($"Номер последнего чека за смену: {connector.ShiftClosingCheckNumber}");
            Console.WriteLine($"Дата и время формирования фискального документа: {connector.DateTime}");
            Console.WriteLine($"Состояние смены: {connector.ShiftState}");
            Console.WriteLine($"Остаток наличных денежных средств в денежном ящике: {connector.CashBalance}");
            Console.WriteLine($"Срок действия ФН: {connector.FnValidityDate}");
            Console.WriteLine($"Количество непереданных документов: {connector.DocumentsCounter}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
