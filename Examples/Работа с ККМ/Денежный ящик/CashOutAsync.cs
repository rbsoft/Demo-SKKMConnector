using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashOutAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Асинхронная выемка наличных";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> PostCashOutAsync()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        // Сумма выемки наличных из денежного ящика (руб.):
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