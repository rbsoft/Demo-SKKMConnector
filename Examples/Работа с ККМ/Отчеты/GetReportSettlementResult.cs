using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportSettlementResult : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Отчёт расчётов по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetReportSettlementById()
    {
        // Идентификатор документа:
        connector.DocId = documentId;

        // Получаем результат отчёта по ИД документа:
        if (await connector.GetReportSettlementResult())
        {
            Console.WriteLine($"Признак удачного завершения обработки: {connector.IsProcessed}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
