using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCorrection105 : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.0.5";
    public const string Title = "Получение чека коррекции 1.0.5";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetCheckCorrection105ById()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем чек коррекции:
        if (await connector.GetCorrection105())
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
            Console.WriteLine($"Электронный чек: {connector.IsElectronically}");
            Console.WriteLine($"Система налогообложения: {connector.TaxVariant}");
            Console.WriteLine($"Тип корректировки: {connector.CorrectionDataType}");
            Console.WriteLine($"Основание коррекции: {connector.CorrectionDataDescription}");
            Console.WriteLine($"Дата корректируемого расчёта: {connector.CorrectionDataDate}");
            Console.WriteLine($"Номер предписания: {connector.CorrectionDataNumber}");
            Console.WriteLine($"Сумма наличными: {connector.Cash}");
            Console.WriteLine($"Сумма безналичными: {connector.ElectronicPayment}");
            Console.WriteLine($"Сумма авансом: {connector.AdvancePayment}");
            Console.WriteLine($"Сумма в кредит: {connector.Credit}");
            Console.WriteLine($"Сумма встречным предоставлением: {connector.CashProvision}");
            Console.WriteLine($"Сумма расчёта без НДС: {connector.SumTaxNone}");
            Console.WriteLine($"Сумма расчёта по ставке НДС 0%: {connector.SumTax0}");
            Console.WriteLine($"Сумма НДС 5%: {connector.SumTax5}");
            Console.WriteLine($"Сумма НДС 7%: {connector.SumTax7}");
            Console.WriteLine($"Сумма НДС 10%: {connector.SumTax10}");
            Console.WriteLine($"Сумма НДС 18%: {connector.SumTax18}");
            Console.WriteLine($"Сумма НДС 20%: {connector.SumTax20}");
            Console.WriteLine($"Сумма НДС 22%: {connector.SumTax22}");
            Console.WriteLine($"Сумма НДС 5/105: {connector.SumTax105}");
            Console.WriteLine($"Сумма НДС 7/107: {connector.SumTax107}");
            Console.WriteLine($"Сумма НДС 10/110: {connector.SumTax110}");
            Console.WriteLine($"Сумма НДС 18/118: {connector.SumTax118}");
            Console.WriteLine($"Сумма НДС 20/120: {connector.SumTax120}");
            Console.WriteLine($"Сумма НДС 22/122: {connector.SumTax122}");
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

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
