using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CloseShift : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Закрыть смену";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> PostCloseShift()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        if (await connector.CloseShift())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"Сумма внесений: {connector.ShiftIncomeSum}");
            Console.WriteLine($"Кол-во внесений: {connector.ShiftIncomeCount}");
            Console.WriteLine($"Сумма выемок: {connector.ShiftOutcomeSum}");
            Console.WriteLine($"Кол-во выемок: {connector.ShiftOutcomeCount}");
            foreach (var counter in connector.Counter)
                Console.WriteLine($"Операция: {counter.Operation} кол-во: {counter.Count} сумма: {counter.Sum} корр. кол-во: {counter.SalesCorrectionCount} корр. сумма: {counter.SalesCorrectionSum} наличные: {counter.Cash} безнал: {counter.Electronically} аванс: {counter.Prepaid} кредит: {counter.Credit} бартер: {counter.Barter} скидка сумма: {counter.DiscountSum} скидка кол-во: {counter.DiscountCount} надбавка сумма: {counter.AddingSum} надбавка кол-во: {counter.AddingCount}");
            Console.WriteLine($"Версия ФФД: {connector.DeviceFfdVersion}");
            Console.WriteLine($"Версия ФФД ФН: {connector.DeviceFnFfdVersion}");
            Console.WriteLine($"Часовая зона: {connector.DeviceTimeZone}");
            Console.WriteLine($"Фискальный режим: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Ширина чековой ленты: {connector.DeviceLineLength}");
            Console.WriteLine($"Ширина чековой ленты в пикселях: {connector.DeviceLineLengthPixels}");
            Console.WriteLine($"Тип устройства: {connector.DeviceClass}");
            Console.WriteLine($"Название модели ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Версия прошивки: {connector.DeviceFirmwareVersion}");
            Console.WriteLine($"Версия конфигурации прошивки устройства: {connector.DeviceConfigurationVersion}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}