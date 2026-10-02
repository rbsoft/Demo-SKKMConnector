using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetQueue : Sample
{
    public const string GroupPath = "Работа с ККМ|Очередь";
    public const string Title = "Состояние очереди";
    public const bool NeedDevice = false;

    public async Task<SkkmConnector> GetGetQueue()
    {
        await connector.GetQueue();
        if (connector.Ok)
        {
            Console.WriteLine($"Заданий в очереди: {connector.Queue.Length}");
            foreach (var q in connector.Queue)
                Console.WriteLine(
                    $"  docId={q.DocId}, касса={q.DeviceName}, пул={q.PoolId}\n" +
                    $"    поставлено={q.Time:dd.MM.yyyy HH:mm:ss}, отправлено на печать={q.SentToPrint}\n" +
                    $"    напечатан={q.Printed}, время печати={q.PrintedTime:dd.MM.yyyy HH:mm:ss}\n" +
                    $"    смена={q.Session}, ФД={q.DocNumber}, сумма={q.Sum}, ошибка={q.ErrorDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
