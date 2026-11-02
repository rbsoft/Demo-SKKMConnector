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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        if (await connector.OpenShift())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Версия ФФД: {connector.DeviceFfdVersion}");
            Console.WriteLine($"Версия ФФД ФН: {connector.DeviceFnFfdVersion}");
            Console.WriteLine($"Часовая зона: {connector.DeviceTimeZone}");
            Console.WriteLine($"Фискальный режим: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Ширина чековой ленты: {connector.DeviceLineLength}");
            Console.WriteLine($"Ширина чековой ленты в пикселях: {connector.DeviceLineLengthPixels}");
            Console.WriteLine($"Тип устройства: {connector.DeviceClass}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Версия прошивки: {connector.DeviceFirmwareVersion}");
            Console.WriteLine($"Версия конфигурации прошивки устройства: {connector.DeviceConfigurationVersion}");
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

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}