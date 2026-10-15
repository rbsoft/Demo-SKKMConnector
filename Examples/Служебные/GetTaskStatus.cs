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
        connector.DeviceName = deviceName;
        await connector.GetTaskStatus(documentId);
        if (connector.Ok)
            Console.WriteLine(
                "Статус задания получен.\n" +
                $"docId: {connector.TaskStatusDocId}\n" +
                $"Имя кассы: {connector.TaskStatusDeviceName}\n" +
                $"Статус отправки: {connector.TaskStatusSentToPrint}\n" +
                $"Позиция в очереди: {connector.TaskStatusNumberInQueue}\n" +
                $"Размер очереди: {connector.TaskStatusQueueSize}\n" +
                $"Номер смены: {connector.TaskStatusShiftNumber}\n" +
                $"Номер ФД: {connector.TaskStatusDocNumber}\n" +
                $"Тип задания: {connector.TaskStatusTaskType}\n" +
                $"Фискальный признак: {connector.TaskStatusFiscalSign}\n" +
                $"Код результата: {connector.TaskStatusResultCode}, {connector.TaskStatusResultDescription}\n" +
                $"Дата постановки: {connector.TaskStatusDate:dd.MM.yyyy HH:mm}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
