using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCashInList : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Список внесений";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetCashInListByDevice()
    {
        connector.DeviceName = deviceName;
        await connector.GetCashInList();
        if (connector.Ok)
        {
            Console.WriteLine($"Внесений: {connector.CashIns.Count}");
            foreach (var check in connector.CashIns)
                Console.WriteLine(
                    $"docId: {check.DocId}, дата: {check.Date:dd.MM.yyyy HH:mm}\n" +
                    $"  Сумма внесения: {check.Sum}, остаток наличных: {check.CashSum}, ящик: {check.DrawerNumber}\n" +
                    $"  ФД: {check.DocNumber}, № за смену: {check.DocNumberInShift}, смена: {check.ShiftNumber}, ФП: {check.FiscalSign}\n" +
                    $"  Дата по часам ФН: {check.FiscalDate:dd.MM.yyyy HH:mm}, ФН: {check.Fn}\n" +
                    $"  Кассир: {check.CashierName}, ИНН: {check.CashierVatin}\n" +
                    $"  Касса: {check.DeviceName}, обработан: {check.IsProcessed}, код: {check.ResultCode} {check.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
