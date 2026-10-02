using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOpenShiftList : Sample
{
    public const string GroupPath = "Работа с ККМ|Кассовые смены";
    public const string Title = "Список открытий смен";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetOpenShiftListByPeriod()
    {
        connector.DeviceName = deviceName;
        connector.FromTo(fromDate, toDate);
        await connector.GetOpenShiftList();
        if (connector.Ok)
        {
            Console.WriteLine($"Открытий смен: {connector.Shifts.Length}");
            foreach (var s in connector.Shifts)
                Console.WriteLine($"  смена {s.ShiftNumber}, docId={s.DocId}, дата={s.Date:dd.MM.yyyy HH:mm}, касса={s.DeviceName}, терминал={s.TerminalId}, код={s.ResultCode} {s.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
