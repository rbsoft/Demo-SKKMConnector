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
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Закрываем смену:
        if (await connector.CloseShift())
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
            Console.WriteLine($"Сумма внесений: {connector.ShiftIncomeSum}");
            Console.WriteLine($"Кол-во внесений: {connector.ShiftIncomeCount}");
            Console.WriteLine($"Сумма выемок: {connector.ShiftOutcomeSum}");
            Console.WriteLine($"Кол-во выемок: {connector.ShiftOutcomeCount}");
            foreach (var row in connector.Counter)
            {
                Console.WriteLine($"Тип операции: {row.Operation}");
                Console.WriteLine($"Количество операций: {row.Count}");
                Console.WriteLine($"Сумма операций: {row.Sum}");
                Console.WriteLine($"Кол-во коррекций: {row.SalesCorrectionCount}");
                Console.WriteLine($"Сумма коррекций: {row.SalesCorrectionSum}");
                Console.WriteLine($"Сумма оплат наличными: {row.Cash}");
                Console.WriteLine($"Сумма оплат безналичными: {row.Electronically}");
                Console.WriteLine($"Сумма оплат авансом: {row.Prepaid}");
                Console.WriteLine($"Сумма оплат в кредит: {row.Credit}");
                Console.WriteLine($"Сумма оплат встречным предоставлением / бартером: {row.Barter}");
                Console.WriteLine($"Сумма предоставленных скидок: {row.DiscountSum}");
                Console.WriteLine($"Кол-во предоставленных скидок: {row.DiscountCount}");
                Console.WriteLine($"Сумма надбавок: {row.AddingSum}");
                Console.WriteLine($"Кол-во надбавок: {row.AddingCount}");
            }

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}