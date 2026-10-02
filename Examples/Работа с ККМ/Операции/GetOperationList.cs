using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationList : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "Список операций";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> GetGetOperationList()
    {
        connector.FromTo(fromDate, toDate);
        await connector.GetOperationList();
        if (connector.Ok)
        {
            Console.WriteLine($"Операций: {connector.Operations.Length}");
            foreach (var o in connector.Operations)
                Console.WriteLine(
                    $"  docId={o.DocId}, {o.TaskName} (тип {o.TaskType}), сумма={o.Sum}\n" +
                    $"    дата={o.Date:dd.MM.yyyy HH:mm}, создана={o.CreatedAt:dd.MM.yyyy HH:mm}, изменена={o.UpdateAt:dd.MM.yyyy HH:mm}\n" +
                    $"    ФД={o.DocNumber}, № за смену={o.DocNumberInShift}, смена={o.SessionNumber}, ФП={o.FiscalSign}, " +
                    $"дата по часам ФН={o.FnDate:dd.MM.yyyy HH:mm}\n" +
                    $"    ФН={o.Fn}, РНМ={o.RnKKT}, ЗН ККТ={o.ZnKKT}\n" +
                    $"    кассир={o.CashierName}, контакт клиента={o.ClientContact}\n" +
                    $"    касса={o.DeviceName}, пул={o.PoolId}, терминал={o.TerminalId}, базовый docId={o.BaseDocId}, запрос={o.RequestId}\n" +
                    $"    обработана={o.Processed}, код={o.ResultCode} {o.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
