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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        if (await connector.CloseShiftAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
