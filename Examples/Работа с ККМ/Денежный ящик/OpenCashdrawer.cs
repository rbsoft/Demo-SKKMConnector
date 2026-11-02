using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OpenCashdrawer : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Открыть ящик";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostOpenCashdrawer()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО и ИНН кассира (ИНН необязателен):
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        // Открываем денежный ящик:
        if (await connector.OpenCashdrawer())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Номер денежного ящика: {connector.DrawerNumber}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
