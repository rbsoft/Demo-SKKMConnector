using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetQueueTask : Sample
{
    public const string GroupPath = "Работа с ККМ|Очередь";
    public const string Title = "Актуальный статус задачи";
    public const bool NeedDocumentId = true;

    public async Task<SkkmConnector> GetGetQueueTask()
    {
        // Идентификатор документа
        connector.DocId = documentId;

        if (await connector.GetQueueTask())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            foreach (var report in connector.ReportValues)
                Console.WriteLine($"{report.Key}: {report.Value} — {report.Description}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
