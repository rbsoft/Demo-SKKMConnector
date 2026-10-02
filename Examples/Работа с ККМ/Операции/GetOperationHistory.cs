using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationHistory : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "История операции";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetGetOperationHistory()
    {
        await connector.GetOperationHistory(documentId);
        if (connector.Ok)
        {
            Console.WriteLine($"Записей истории: {connector.OperationHistory.Count}");
            foreach (var h in connector.OperationHistory)
            {
                Console.WriteLine($"  {h.Time:dd.MM.yyyy HH:mm:ss}: состояние {h.State} — {h.Description}");
                if (h.Document != null)
                    Console.WriteLine(
                        $"    документ: docId={h.Document.DocId}, ФД={h.Document.DocNumber}, ФП={h.Document.FiscalSign}, " +
                        $"смена={h.Document.ShiftNumber}, сумма={h.Document.Sum}, код={h.Document.ResultCode} {h.Document.ResultDescription}");
            }
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
