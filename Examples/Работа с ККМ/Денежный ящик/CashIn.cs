using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashIn : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Внесение";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostCashIn()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Сумма внесения:
        connector.CashAmount = cashAmount;

        // Вносим наличные:
        if (await connector.CashIn())
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