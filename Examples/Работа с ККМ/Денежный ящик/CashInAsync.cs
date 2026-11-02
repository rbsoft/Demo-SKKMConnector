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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.CashAmount = cashAmount;
        if (await connector.CashInAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
