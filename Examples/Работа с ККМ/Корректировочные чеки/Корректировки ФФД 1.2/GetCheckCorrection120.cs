using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckCorrection120 : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Получение чека коррекции 1.2";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetCheckCorrection120ById()
    {
        connector.DeviceName = deviceName;
        await connector.GetCorrection120(documentId);

        if (connector.Ok)
        {
            Console.WriteLine(
                "Чек коррекции 1.2 получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер ФД за смену: {connector.CheckDocNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Сумма чека: {connector.CheckSum}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"СНО: {connector.CheckTaxType}\n" +
                $"Тип чека: {connector.CheckTaskType}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                "--- Данные коррекции ---\n" +
                $"Тип коррекции: {connector.CorrectionDataType}\n" +
                $"Основание: {connector.CorrectionDataDescription}\n" +
                $"Дата корректируемого расчёта: {connector.CorrectionDataDate:dd.MM.yyyy}\n" +
                $"Номер предписания: {connector.CorrectionDataNumber}\n" +
                "--- Реквизиты ---\n" +
                $"Оплата наличными: {connector.PaymentsCash}, безнал: {connector.PaymentsElectronic}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"ОФД: {connector.OfdName}, ИНН ОФД: {connector.OfdVatin}\n" +
                $"Устройство: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, ФФД {connector.DeviceFfdVersion})");

            foreach (var item in connector.CheckItems)
                Console.WriteLine($"  {item.Name}: кол-во={item.Quantity}, цена={item.Price}, сумма={item.Sum}, НДС={item.TaxValue}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
