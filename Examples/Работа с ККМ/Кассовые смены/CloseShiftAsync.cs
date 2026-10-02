using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CloseShiftAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Асинхронное закрытие смены";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> PostCloseShiftAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.CloseShiftAsync();
        if (connector.Ok)
            Console.WriteLine($"Закрытие смены поставлено в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
