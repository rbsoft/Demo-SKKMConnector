using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashInAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Асинхронное внесение наличных";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostCashInAsync()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        // Сумма внесения наличных в денежный ящик (руб.):
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