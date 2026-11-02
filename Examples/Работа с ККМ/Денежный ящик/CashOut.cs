using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashOut : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Выемка";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> PostCashOut()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Сумма выемки:
        connector.CashAmount = cashAmount;

        // Выполняем выемку:
        if (await connector.CashOut())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер чека внесения: {connector.CheckNumber}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}