using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashOut : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Выемка";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> PostCashOut()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;

        // Сумма выемки наличных из денежного ящика (руб.):
        connector.CashAmount = cashAmount;
        if (await connector.CashOut())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Тип документа (чека): {connector.TaskType}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Фискальный признак чека: {connector.FiscalSign}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Версия ФФД документа: {connector.FnFFDVersion}");
            Console.WriteLine($"Адрес проведения расчётов: {connector.FnSaleAddress}");
            Console.WriteLine($"Место проведения расчётов: {connector.FnSaleLocation}");
            Console.WriteLine($"Организация: {connector.OrganizationInfo}");
            Console.WriteLine($"ИНН организации: {connector.FnVatin}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Адрес сайта уполномоченного органа (ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Наименование провайдера ОФД: {connector.FnOfdName}");
            Console.WriteLine($"ИНН провайдера ОФД: {connector.FnOfdVatin}");
            Console.WriteLine($"ФИО кассира: {connector.CashierName}");
            Console.WriteLine($"ИНН кассира: {connector.CashierVatin}");
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Идентификатор рабочего места (терминала): {connector.TerminalId}");
            Console.WriteLine($"Идентификатор пула: {connector.PoolId}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Описание результата: {connector.ResultDescription}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");
            Console.WriteLine($"Версия Сервера ККМ: {connector.ServerVersion}");
            Console.WriteLine($"Признак электронного чека (без печати на бумаге): {connector.IsElectronically}");
            Console.WriteLine($"Система налогообложения: {connector.TaxVariant}");
            Console.WriteLine($"Сумма оплаты наличными: {connector.Cash}");
            Console.WriteLine($"Сумма оплаты безналичными: {connector.ElectronicPayment}");
            Console.WriteLine($"Сумма оплаты авансом: {connector.AdvancePayment}");
            Console.WriteLine($"Сумма оплаты в кредит: {connector.Credit}");
            Console.WriteLine($"Сумма оплаты встречным предоставлением: {connector.CashProvision}");
            Console.WriteLine($"Сумма: {connector.CashAmount}");
            Console.WriteLine($"Название модели ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"Версия прошивки: {connector.DeviceFirmwareVersion}");
            Console.WriteLine($"Версия конфигурации прошивки устройства: {connector.DeviceConfigurationVersion}");
            Console.WriteLine($"Версия ФФД ККТ: {connector.DeviceFfdVersion}");
            Console.WriteLine($"Версия ФФД ФН: {connector.DeviceFnFfdVersion}");
            Console.WriteLine($"Тип устройства: {connector.DeviceClass}");
            Console.WriteLine($"Часовая зона ККТ: {connector.DeviceTimeZone}");
            Console.WriteLine($"Фискальный режим: {connector.DeviceIsFiscal}");
            Console.WriteLine($"Ширина строки чека (в символах): {connector.DeviceLineLength}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}