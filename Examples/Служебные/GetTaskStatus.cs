using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetTaskStatus : Sample
{
    public const string GroupPath = "Служебные";
    public const string Title = "Статус задания";
    public const int SortOrder = 2;
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetGetTaskStatus()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Узнаём статус задания (ИД документа передаётся параметром):
        if (await connector.GetTaskStatus())
        {
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Дата и время постановки задания в обработку: {connector.DateTime}");
            Console.WriteLine($"Статус отправки: {connector.SentToPrint}");
            Console.WriteLine($"Позиция задания в очереди на момент запроса: {connector.NumberInQueue}");
            Console.WriteLine($"Размер очереди: {connector.QueueSize}");
            Console.WriteLine($"Идентификатор пула: {connector.PoolId}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Тип чека: {connector.TaskType}");
            Console.WriteLine($"Фискальный признак: {connector.FiscalSign}");
            Console.WriteLine($"Название организации: {connector.OrganizationInfo}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");
            Console.WriteLine($"ИНН Организации: {connector.FnVatin}");
            Console.WriteLine($"ФИО кассира: {connector.CashierName}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Фискальный накопитель: {connector.FnSerialNumber}");
            Console.WriteLine($"Адрес сайта уполномоченного органа (ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Наименование провайдера ОФД: {connector.FnOfdName}");
            Console.WriteLine($"ИНН провайдера ОФД: {connector.FnOfdVatin}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
