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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.CashAmount = cashAmount;
        if (await connector.CashOutAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
