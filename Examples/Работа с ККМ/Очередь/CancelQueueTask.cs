using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CancelQueueTask : Sample
{
    public const string GroupPath = "Работа с ККМ|Очередь";
    public const string Title = "Удаление задания из очереди";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> DeleteCancelQueueTask()
    {
        connector.QueueTaskId = documentId;
        await connector.CancelQueueTask();
        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
