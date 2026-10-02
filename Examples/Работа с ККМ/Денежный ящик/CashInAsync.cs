using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashInAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Асинхронное внесение наличных";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostCashInAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.CashAmount = 1000;
        await connector.CashInAsync();
        if (connector.Ok)
            Console.WriteLine($"Внесение поставлено в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
