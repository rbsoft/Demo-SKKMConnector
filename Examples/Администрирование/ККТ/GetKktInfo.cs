using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetKktInfo : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Информация о ККТ";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetConnect()
    {
        connector.DeviceName = deviceName;
        await connector.GetKktInfo();

        if (connector.Ok)
            Console.WriteLine(
                "Информация о ККТ получена.\n" +
                $"Версия сервера ККМ: {connector.ServerVersion}\n" +
                "--- Устройство ---\n" +
                $"Модель: {connector.DeviceModel} ({connector.DeviceModelName})\n" +
                $"Заводской номер: {connector.DeviceSerialNumber}\n" +
                $"Прошивка: {connector.DeviceFirmwareVersion}, конфигурация: {connector.DeviceConfigurationVersion}\n" +
                $"Версия ФФД ККТ: {connector.DeviceFfdVersion}, ФФД ФН: {connector.FnFfdVersion}\n" +
                $"Класс устройства: {connector.DeviceClass}, часовая зона: {connector.DeviceTimeZone}\n" +
                $"Фискальный режим: {connector.IsFiscal}\n" +
                $"Ширина строки: {connector.LineLength} симв. / {connector.LineLengthPixels} пикс.\n" +
                $"Лицензий ККТ: {connector.DeviceLicenses.Count}\n" +
                $"Драйвер: {connector.DriverType}, версия {connector.DriverVersion}, {connector.DriverVendor}\n" +
                "--- Фискальный накопитель ---\n" +
                $"Номер ФН: {connector.FnNumber}, версия: {connector.FnVersion}, исполнение: {connector.FnExecution}\n" +
                $"Фаза жизни: {connector.FnLivePhase}\n" +
                $"Срок действия ФН: {connector.FnValidityDate} (осталось дней: {connector.FnDaysResources})\n" +
                $"РНМ: {connector.RnNumber}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"Адрес расчётов: {connector.FnSaleAddress}\n" +
                $"Место расчётов: {connector.FnSaleLocation}\n" +
                $"СНО (код): {connector.FnTaxVariant}, признак агента: {connector.FnSignOfAgent}\n" +
                $"Номер автомата: {connector.FnAutomaticNumber}\n" +
                $"Почта отправителя: {connector.FnSenderEmail}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Задан сервер обновления ключей: {connector.FnContainsKeysUpdaterServerUri}\n" +
                $"Фискализаций: {connector.FnFiscalizationsCount}, осталось: {connector.FnFiscalizationsFree}\n" +
                $"Последняя фискализация: {connector.FnFiscalizationDateTime:dd.MM.yyyy HH:mm}, ФД {connector.FnFiscalizationDocumentNumber}, причина {connector.FnReasonCode}\n" +
                $"ОФД: {connector.OfdName}, ИНН: {connector.OfdVatin}, {connector.OfdHost}:{connector.OfdPort}\n" +
                "--- Статус ---\n" +
                $"Номер смены: {connector.StatusShiftNumber}\n" +
                $"Номер последнего ФД: {connector.StatusDocNumber}\n" +
                $"Смена открыта: {connector.StatusIsShiftOpened}, истекла: {connector.StatusIsShiftExpired}\n" +
                $"Время ПК: {connector.StatusComputerTime:dd.MM.yyyy HH:mm:ss}, время ККТ: {connector.StatusDeviceTime:dd.MM.yyyy HH:mm:ss}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
                $"Непереданных в ОФД: {connector.BacklogDocumentsCounter}\n" +
                "--- Режимы ФН ---\n" +
                $"Маркировка: {connector.FnModesIsMarking}\n" +
                $"Подакцизные товары: {connector.FnModesIsSaleExcisableGoods}\n" +
                $"БСО: {connector.FnModesIsBsoSign}\n" +
                $"Автономный режим: {connector.FnModesIsOfflineMode}\n" +
                $"Расчёты в интернете: {connector.FnModesIsCalcOnlineSign}\n" +
                $"Услуги: {connector.FnModesIsServiceSign}\n" +
                $"Общепит: {connector.FnModesIsCateringServices}\n" +
                $"Опт: {connector.FnModesIsWholesaleTrade}\n" +
                $"Шифрование: {connector.FnModesIsDataEncryption}\n" +
                $"Автоматический принтер: {connector.FnModesIsPrinterAutomatic}\n" +
                $"Автоматический режим: {connector.FnModesIsAutomaticMode}\n" +
                $"Торговый автомат: {connector.FnModesIsVendingMachine}\n" +
                $"Азартные игры: {connector.FnModesIsSignOfGambling}\n" +
                $"Лотереи: {connector.FnModesIsSignOfLottery}\n" +
                $"Ломбард: {connector.FnModesIsPawnshop}\n" +
                $"Страхование: {connector.FnModesIsAssurance}\n" +
                "--- Предупреждения ФН ---\n" +
                $"Критическая ошибка: {connector.FnWarningsIsCriticalError}\n" +
                $"Память переполнена: {connector.FnWarningsIsMemoryOverflow}\n" +
                $"Требуется замена: {connector.FnWarningsIsNeedReplacement}\n" +
                $"Таймаут ОФД: {connector.FnWarningsIsOfdTimeout}\n" +
                $"Исчерпан ресурс: {connector.FnWarningsIsResourceExhausted}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}