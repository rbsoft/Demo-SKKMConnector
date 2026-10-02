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
        await connector.GetShiftStatus();

        if (connector.Ok)
            Console.WriteLine(
                "Статус смены получен.\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер последнего ФД: {connector.CheckNumber}\n" +
                $"Состояние смены: {connector.CurrentShiftState}\n" +
                $"Непереданных в ОФД: {connector.BacklogDocumentsCounter}\n" +
                $"Номер первого непереданного: {connector.BacklogDocumentFirstNumber}\n" +
                $"Дата первого непереданного: {connector.BacklogDocumentFirstDateTime:dd.MM.yyyy HH:mm}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}