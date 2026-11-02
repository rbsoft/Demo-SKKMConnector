using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class ShiftStatus : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Статус смены";
    public const int SortOrder = 8;

    public async Task<SkkmConnector> GetShiftStatus()
    {
        connector.DeviceName = deviceName;
        if (await connector.GetShiftStatus())
        {
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Состояние смены: {connector.ShiftState}");
            Console.WriteLine($"Количество непереданных документов: {connector.BacklogDocumentsCounter}");
            Console.WriteLine($"Номер первого непереданного чека: {connector.BacklogDocumentFirstNumber}");
            Console.WriteLine($"Дата первого непереданного чека: {connector.BacklogDocumentFirstDateTime}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}