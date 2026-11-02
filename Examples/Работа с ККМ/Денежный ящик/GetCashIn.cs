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
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем результат внесения:
        if (await connector.GetCashIn())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер чека внесения: {connector.CheckNumber}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}
