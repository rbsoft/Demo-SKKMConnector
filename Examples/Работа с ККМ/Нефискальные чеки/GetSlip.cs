using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetSlip : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Получение нефискального документа";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetSlip()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем слип-чек:
        if (await connector.GetSlip())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
