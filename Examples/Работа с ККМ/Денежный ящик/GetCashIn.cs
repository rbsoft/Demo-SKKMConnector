using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCashIn : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Внесение по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 5;

    public async Task<SkkmConnector> GetCashInById()
    {
        connector.DeviceName = deviceName;
        await connector.GetCashIn(documentId);

        if (connector.Ok)
            Console.WriteLine(
                "Документ внесения получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Дата создания: {connector.CheckDate:dd.MM.yyyy HH:mm}\n" +
                $"Тип документа: {connector.CheckTaskType}\n" +
                $"Сумма внесения: {connector.CheckSum}\n" +
                $"Остаток наличных: {connector.CheckCashSum}\n" +
                $"Номер денежного ящика: {connector.CheckDrawerNumber}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер ФД за смену: {connector.CheckDocNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"Адрес расчётов: {connector.CheckSaleAddress}\n" +
                $"Место расчётов: {connector.CheckSaleLocation}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                $"Имя кассы: {connector.CheckDeviceName}, пул: {connector.CheckPoolId}, терминал: {connector.CheckTerminalId}\n" +
                $"Обработан успешно: {connector.CheckIsProcessed}, код: {connector.CheckResultCode} {connector.CheckResultDescription}\n" +
                $"Версия сервера ККМ: {connector.CheckServerVersion}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"РНМ: {connector.RnNumber}, ФН: {connector.FnNumber}\n" +
                $"ОФД: {connector.OfdName}, ИНН ОФД: {connector.OfdVatin}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Устройство: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, ФФД {connector.DeviceFfdVersion})");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
