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
        // Идентификатор документа внесения:
        connector.DocId = documentId;
        if (await connector.GetCashIn())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Тип документа: {connector.TaskType}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека внесения: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Сумма внесения: {connector.CashAmount}");
            Console.WriteLine($"Номер ФН: {connector.FnSerialNumber}");
            Console.WriteLine($"Организация: {connector.OrganizationInfo}");
            Console.WriteLine($"ФИО кассира: {connector.CashierName}");
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Описание результата: {connector.ResultDescription}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");
            Console.WriteLine($"Модель ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
