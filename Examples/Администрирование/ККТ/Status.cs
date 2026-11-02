using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Status : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Статус ККМ";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> GetStatus()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Получаем расширенное состояние ККТ:
        if (await connector.GetStatus())
        {
            Console.WriteLine($"Фискальный режим: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Не доступна информационная система маркировки: {connector.IsIsmDisconnected}");
            Console.WriteLine($"Не доступен оператор информационной системы маркировки, оператор фискальных данных (ОФД): {connector.IsOfdDisconnected}");
            Console.WriteLine($"Находится ли фискальный накопитель в состоянии ошибки: {connector.IsFnError}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Сумма выручки: {connector.TotalSum}");
            Console.WriteLine($"Время открытия кассовой смены: {connector.OpenShiftDate}");
            Console.WriteLine($"Смена открыта: {connector.IsShiftOpened}");
            Console.WriteLine($"Смена истекла: {connector.IsShiftExpired}");
            Console.WriteLine($"Код ошибки: {connector.ErrorCode}");
            Console.WriteLine($"Описание ошибки устройства по данным драйвера: {connector.ErrorCodeDescription}");
            Console.WriteLine($"Ширина чековой ленты в символах: {connector.DeviceLineLength}");
            Console.WriteLine($"Статус лицензии Сервера ККМ: {connector.LicenseStatus}");
            Console.WriteLine($"Состояние смены: {connector.ShiftState}");
            Console.WriteLine($"Сумма наличных в денежном ящике: {connector.CashBalance}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Присутствует ли фискальный накопитель: {connector.IsFnPresent}");
            Console.WriteLine($"Открыт денежный ящик: {connector.IsDrawerOpened}");
            Console.WriteLine($"Наличие чековой ленты: {connector.IsCheckPaperPresent}");
            Console.WriteLine($"Наличие контрольной ленты: {connector.IsControlPaperPresent}");
            Console.WriteLine($"Критическая ошибка ФН: {connector.FnWarningsIsCriticalError}");
            Console.WriteLine($"Память ФН переполнена: {connector.FnWarningsIsMemoryOverflow}");
            Console.WriteLine($"Исчерпан ресурс ФН: {connector.FnWarningsIsResourceExhausted}");
            Console.WriteLine($"Требуется срочная замена ФН: {connector.FnWarningsIsNeedReplacement}");
            Console.WriteLine($"Превышено время ожидания ответа от ОФД: {connector.FnWarningsIsOfdTimeout}");
            Console.WriteLine($"Время получения данных, время на Сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Время в часах устройства: {connector.DeviceTime}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
