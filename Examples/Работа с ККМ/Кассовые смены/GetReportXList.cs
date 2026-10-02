using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportXList : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Список X-отчётов";
    public const int SortOrder = 11;

    public async Task<SkkmConnector> GetReportXListByPeriod()
    {
        connector.DeviceName = deviceName;
        connector.FromTo(fromDate, toDate);
        await connector.GetReportXList();
        if (connector.Ok)
        {
            Console.WriteLine($"X-отчётов: {connector.Shifts.Length}");
            foreach (var s in connector.Shifts)
                Console.WriteLine($"  смена {s.ShiftNumber}, docId={s.DocId}, дата={s.Date:dd.MM.yyyy HH:mm}, касса={s.DeviceName}, терминал={s.TerminalId}, код={s.ResultCode} {s.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
