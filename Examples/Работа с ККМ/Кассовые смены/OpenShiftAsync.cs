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
        connector.SetCashier(cashierName, cashierVatin);
        await connector.OpenShiftAsync();
        if (connector.Ok)
            Console.WriteLine($"Открытие смены поставлено в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
