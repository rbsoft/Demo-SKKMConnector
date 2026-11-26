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
            foreach (var license in connector.KktLicenses)
                Console.WriteLine($"Лицензия: {license.Name} №{license.Number} с {license.ValidFrom} по {license.ValidUntil} версия {license.UnitVersion} {license.Description} активна: {license.IsActive}");
            Console.WriteLine($"Тип драйвера: {connector.DriverType}");
            Console.WriteLine($"Версия драйвера: {connector.DriverVendor}");
            Console.WriteLine($"Данные поставщика: {connector.DriverVersion}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}