using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationRelated : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "Связанные операции";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 5;

    public async Task<SkkmConnector> GetGetOperationRelated()
    {
        await connector.GetOperationRelated(documentId);
        if (connector.Ok)
        {
            Console.WriteLine($"Связанных операций: {connector.RelatedOperations.Count}");
            foreach (var r in connector.RelatedOperations)
                Console.WriteLine(
                    $"  docId={r.DocId}, тип={r.TaskType}, дата={r.Date:dd.MM.yyyy HH:mm}\n" +
                    $"    базовый docId={r.BaseDocId}, запрос={r.RequestId}\n" +
                    $"    касса={r.DeviceName}, пул={r.PoolId}, терминал={r.TerminalId}\n" +
                    $"    обработана={r.IsProcessed}, код={r.ResultCode} {r.ResultDescription}\n" +
                    $"    устройство={r.DeviceModel} (сер. {r.DeviceSerialNumber}), отправитель={r.SenderAppName} {r.SenderAppVersion}\n" +
                    $"    версия сервера={r.ServerVersion}, клиента={r.ClientVersion}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
