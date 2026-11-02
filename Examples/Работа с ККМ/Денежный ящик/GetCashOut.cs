using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCashOut : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Выемка по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 8;

    public async Task<SkkmConnector> GetCashOutById()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем результат выемки:
        if (await connector.GetCashOut())
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
