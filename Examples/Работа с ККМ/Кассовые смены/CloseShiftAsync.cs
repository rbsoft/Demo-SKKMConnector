using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CloseShiftAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Асинхронное закрытие смены";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> PostCloseShiftAsync()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        if (await connector.CloseShiftAsync())
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}