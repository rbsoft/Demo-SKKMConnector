using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetReportSettlementList : Sample
{
    public const string GroupPath = "Работа с ККМ|Отчеты";
    public const string Title = "Список отчётов расчётов";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetReportSettlementListByPeriod()
    {
        connector.DeviceName = deviceName;
        connector.FromTo(fromDate, toDate);
        await connector.GetReportSettlementList();
        if (connector.Ok)
        {
            Console.WriteLine($"Отчётов о расчётах: {connector.Shifts.Length}");
            foreach (var s in connector.Shifts)
                Console.WriteLine($"  смена {s.ShiftNumber}, docId={s.DocId}, дата={s.Date:dd.MM.yyyy HH:mm}, касса={s.DeviceName}, терминал={s.TerminalId}, код={s.ResultCode} {s.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
