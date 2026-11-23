using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetSlip : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Получение нефискального документа";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetSlip()
    {
        // DocId слипа :
        connector.DocId = documentId;
        if (await connector.GetSlip())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Тип документа: {connector.TaskType}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Фискальный признак чека: {connector.FiscalSign}");
            Console.WriteLine($"Номер ФН: {connector.FnSerialNumber}");
            Console.WriteLine($"Версия ФФД документа: {connector.FnFFDVersion}");
            Console.WriteLine($"Адрес расчётов: {connector.FnSaleAddress}");
            Console.WriteLine($"Место расчётов: {connector.FnSaleLocation}");
            Console.WriteLine($"Организация: {connector.OrganizationInfo}");
            Console.WriteLine($"ИНН организации: {connector.FnVatin}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Адрес сайта ФНС: {connector.FnFnsUrl}");
            Console.WriteLine($"Наименование ОФД: {connector.FnOfdName}");
            Console.WriteLine($"ИНН ОФД: {connector.FnOfdVatin}");
            Console.WriteLine($"ФИО кассира: {connector.CashierName}");
            Console.WriteLine($"ИНН кассира: {connector.CashierVatin}");
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Идентификатор рабочего места: {connector.TerminalId}");
            Console.WriteLine($"Пул: {connector.PoolId}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Описание результата: {connector.ResultDescription}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");
            Console.WriteLine($"Версия Сервера ККМ: {connector.ServerVersion}");
            Console.WriteLine($"Модель ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Версия прошивки ККТ: {connector.DeviceFirmwareVersion}");
            Console.WriteLine($"Версия конфигурации прошивки: {connector.DeviceConfigurationVersion}");
            Console.WriteLine($"Версия ФФД ККТ: {connector.DeviceFfdVersion}");
            Console.WriteLine($"Версия ФФД ФН: {connector.DeviceFnFfdVersion}");
            Console.WriteLine($"Тип устройства: {connector.DeviceClass}");
            Console.WriteLine($"Часовая зона ККТ: {connector.DeviceTimeZone}");
            Console.WriteLine($"Фискальный режим ККТ: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Ширина строки чека: {connector.DeviceLineLength}");
            Console.WriteLine($"Ширина чековой ленты в пикселях: {connector.DeviceLineLengthPixels}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
