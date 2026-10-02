using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetQueueTaskHistory : Sample
{
    public const string GroupPath = "Работа с ККМ|Очередь";
    public const string Title = "История обработки задания";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetGetQueueTaskHistory()
    {
        await connector.GetQueueTaskHistory(documentId);
        if (connector.Ok)
        {
            Console.WriteLine(
                "История задания очереди получена.\n" +
                $"docId: {connector.QueueTaskDocId}\n" +
                $"Имя кассы: {connector.QueueTaskDeviceName}\n" +
                $"Состояние документа (код): {connector.QueueTaskDocState}\n" +
                $"Состояние очереди (код): {connector.QueueTaskQueueState}\n" +
                $"Позиция в очереди: {connector.QueueTaskNumberInQueue}\n" +
                $"Этап обработки: {connector.QueueTaskPrintStatusDescription}\n" +
                $"Фискальный признак: {connector.QueueTaskFiscalSign}\n" +
                $"Код результата: {connector.QueueTaskResultCode}, {connector.QueueTaskResultDescription}\n" +
                $"Дата изменения статуса: {connector.QueueTaskDate:dd.MM.yyyy HH:mm}\n" +
                $"Записей истории: {connector.OperationHistory.Count}");
            foreach (var h in connector.OperationHistory)
                Console.WriteLine($"  {h.Time:dd.MM.yyyy HH:mm:ss}: состояние {h.State} — {h.Description} {h.Info}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
