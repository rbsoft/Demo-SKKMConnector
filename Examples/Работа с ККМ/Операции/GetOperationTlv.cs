using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationTlv : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "TLV операции";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetOperationTlv()
    {
        await connector.GetOperationTlv(documentId);
        if (connector.Ok)
            Console.WriteLine($"TLV: {connector.OperationTlv}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
