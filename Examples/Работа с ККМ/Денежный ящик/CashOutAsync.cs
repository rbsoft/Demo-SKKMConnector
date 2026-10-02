using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashOutAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Асинхронная выемка наличных";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> PostCashOutAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.CashAmount = 500;
        await connector.CashOutAsync();
        if (connector.Ok)
            Console.WriteLine($"Выемка поставлена в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
