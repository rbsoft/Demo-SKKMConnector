using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetQueueTask : Sample
{
    public const string GroupPath = "Работа с ККМ|Очередь";
    public const string Title = "Актуальный статус задачи";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetGetQueueTask()
    {
        await connector.GetQueueTask(documentId);
        if (connector.Ok)
            Console.WriteLine(
                "Задание очереди получено.\n" +
                $"docId: {connector.QueueTaskDocId}\n" +
                $"Имя кассы: {connector.QueueTaskDeviceName}\n" +
                $"Состояние документа (код): {connector.QueueTaskDocState}\n" +
                $"Состояние очереди (код): {connector.QueueTaskQueueState}\n" +
                $"Позиция в очереди: {connector.QueueTaskNumberInQueue}\n" +
                $"Этап обработки: {connector.QueueTaskPrintStatusDescription}\n" +
                $"Фискальный признак: {connector.QueueTaskFiscalSign}\n" +
                $"Код результата: {connector.QueueTaskResultCode}, {connector.QueueTaskResultDescription}\n" +
                $"Дата изменения статуса: {connector.QueueTaskDate:dd.MM.yyyy HH:mm}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
