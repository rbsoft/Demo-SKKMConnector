using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Status : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Статус ККМ";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> GetStatus()
    {
        connector.DeviceName = deviceName;
        await connector.GetStatus();

        if (connector.Ok)
            Console.WriteLine(
                "Статус ККТ получен.\n" +
                $"Номер смены: {connector.StatusShiftNumber}\n" +
                $"Номер последнего ФД: {connector.StatusDocNumber}\n" +
                $"Номер ФД за смену: {connector.StatusDocNumberInShift}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
                $"Смена открыта: {connector.StatusIsShiftOpened}, истекла: {connector.StatusIsShiftExpired}\n" +
                $"Время открытия смены: {connector.StatusOpenShiftTime:dd.MM.yyyy HH:mm:ss}\n" +
                $"Наличные в ящике: {connector.StatusCashSum}\n" +
                $"Сумма за смену: {connector.StatusTotalSum}\n" +
                $"Время ПК: {connector.StatusComputerTime:dd.MM.yyyy HH:mm:ss}\n" +
                $"Время ККТ: {connector.StatusDeviceTime:dd.MM.yyyy HH:mm:ss}\n" +
                $"Ширина строки: {connector.LineLength} симв. / {connector.LineLengthPixels} пикс.\n" +
                "--- Состояние устройства ---\n" +
                $"Фискальный режим: {connector.StatusIsFiscal}\n" +
                $"ФН присутствует: {connector.StatusIsFnPresent}, ошибка ФН: {connector.StatusIsFnError}\n" +
                $"ОФД недоступен: {connector.StatusIsOfdDisconnected}\n" +
                $"ИСМ недоступна: {connector.StatusIsIsmDisconnected}\n" +
                $"Денежный ящик открыт: {connector.StatusIsDrawerOpened}\n" +
                $"Чековая лента есть: {connector.StatusIsCheckPaperPresent}\n" +
                $"Контрольная лента есть: {connector.StatusIsControlPaperPresent}\n" +
                $"Крышка открыта: {connector.StatusIsCoverOpened}\n" +
                $"Аккумулятор разряжен: {connector.StatusIsBatteryLow}\n" +
                $"Открыт документ: {connector.StatusIsOpenDocument}\n" +
                $"Ожидание продолжения печати: {connector.StatusIsWaitContinuePrint}\n" +
                $"Касса занята: {connector.StatusIsBusy}, задание: {connector.StatusTaskId}\n" +
                $"Ошибка устройства: {connector.StatusError}, код {connector.StatusErrorCode} {connector.StatusErrorCodeDescription}\n" +
                $"Режим драйвера: {connector.StatusDriverMode} {connector.StatusDriverModeDescription}\n" +
                $"Подрежим драйвера: {connector.StatusDriverAdvancedMode} {connector.StatusDriverAdvancedModeDescription}\n" +
                "--- Очередь ОФД ---\n" +
                $"Непереданных документов: {connector.BacklogDocumentsCounter}\n" +
                $"Первый непереданный: №{connector.BacklogDocumentFirstNumber} от {connector.BacklogDocumentFirstDateTime:dd.MM.yyyy HH:mm}\n" +
                "--- Обмен с ИСМ ---\n" +
                $"Адрес ИСМ: {connector.IsmAddress}:{connector.IsmPort}\n" +
                $"Непереданных уведомлений: {connector.IsmBacklogDocumentsCounter}\n" +
                $"Первое непереданное: №{connector.IsmBacklogDocumentFirstNumber} от {connector.IsmBacklogDocumentFirstDateTime:dd.MM.yyyy HH:mm}\n" +
                $"Последнее успешное подключение: {connector.IsmLastSuccessConnectionDateTime:dd.MM.yyyy HH:mm}\n" +
                $"Ошибка: команда ФН {connector.IsmErrorFnCommandCode}, документ №{connector.IsmErrorDocumentNumber}\n" +
                $"  ФН: {connector.IsmErrorFnCode} {connector.IsmErrorFnDescription}\n" +
                $"  Сеть: {connector.IsmErrorNetworkCode} {connector.IsmErrorNetworkDescription}\n" +
                $"  ИСМ: {connector.IsmErrorIsmCode} {connector.IsmErrorIsmDescription}\n" +
                "--- Предупреждения ФН ---\n" +
                $"Критическая ошибка: {connector.FnWarningsIsCriticalError}\n" +
                $"Память переполнена: {connector.FnWarningsIsMemoryOverflow}\n" +
                $"Требуется замена: {connector.FnWarningsIsNeedReplacement}\n" +
                $"Таймаут ОФД: {connector.FnWarningsIsOfdTimeout}\n" +
                $"Исчерпан ресурс: {connector.FnWarningsIsResourceExhausted}\n" +
                "--- Лицензия ---\n" +
                $"Статус лицензии: {connector.LicenseStatus}, обновлена: {connector.LicenseUpdated:dd.MM.yyyy HH:mm}\n" +
                $"Код: {connector.LicenseCode}, конечный пользователь: {connector.LicenseIsEndUser}\n" +
                $"Активирована: {connector.LicenseIsActivated}, заблокирована: {connector.LicenseIsBlocked} ({connector.LicenseBlockDate:dd.MM.yyyy})\n" +
                $"Выдана: {connector.LicenseDate:dd.MM.yyyy}, действует до: {connector.LicenseExpired:dd.MM.yyyy}, обновления до: {connector.LicenseUpdateExpired:dd.MM.yyyy}\n" +
                $"Лимит установок: {connector.LicenseLimitInstalls}, лимит объектов: {connector.LicenseLimitObjects}, " +
                $"нужна активация объекта: {connector.LicenseNeedObjectActivation}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
