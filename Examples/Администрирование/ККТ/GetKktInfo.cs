using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetKktInfo : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Информация о ККТ";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetConnect()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Получаем данные ККТ:
        if (await connector.GetKktInfo())
        {
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
            Console.WriteLine($"Название модели ККТ: {connector.DeviceModel}");
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
            Console.WriteLine($"Тип драйвера: {connector.DriverType}");
            Console.WriteLine($"Данные поставщика: {connector.DriverVendor}");
            Console.WriteLine($"Версия драйвера: {connector.DriverVersion}");
            Console.WriteLine($"Версия Сервера ККМ: {connector.ServerVersion}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Срок действия ФН: {connector.FnValidityDate}");
            Console.WriteLine($"Наименование организации: {connector.OrganizationInfo}");
            Console.WriteLine($"ИНН: {connector.FnVatin}");
            Console.WriteLine($"Название ОФД: {connector.FnOfdName}");
            Console.WriteLine($"ИНН ОФД: {connector.FnOfdVatin}");
            Console.WriteLine($"Адрес расчётов: {connector.FnSaleAddress}");
            Console.WriteLine($"Место расчётов: {connector.FnSaleLocation}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}