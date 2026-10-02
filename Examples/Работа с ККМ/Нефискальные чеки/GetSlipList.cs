using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetSlipList : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Список нефискальных документов";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetGetSlipList()
    {
        connector.DeviceName = deviceName;
        await connector.GetSlipList();
        if (connector.Ok)
        {
            Console.WriteLine($"Слипов: {connector.Slips.Count}");
            foreach (var c in connector.Slips)
                Console.WriteLine(
                    $"  docId={c.DocId}, дата={c.Date:dd.MM.yyyy HH:mm}, тип={c.TaskType}\n" +
                    $"    касса={c.DeviceName}, пул={c.PoolId}, терминал={c.TerminalId}\n" +
                    $"    обработан={c.IsProcessed}, код={c.ResultCode} {c.ResultDescription}, строк={c.LinesCount}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
