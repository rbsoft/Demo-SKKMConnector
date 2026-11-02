using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OpenShiftAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Открыть смену (async)";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostOpenShiftAsync()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        if (await connector.OpenShiftAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
