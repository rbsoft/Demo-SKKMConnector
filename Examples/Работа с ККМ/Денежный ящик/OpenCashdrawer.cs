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
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира:
        connector.CashierVatin = cashierVatin;
        if (await connector.OpenCashdrawer())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Тип документа: {connector.TaskType}");
            Console.WriteLine($"Время на сервере ККМ: {connector.ServerTime}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Номер чека за смену: {connector.CheckNumberInShift}");
            Console.WriteLine($"Номер денежного ящика: {connector.DrawerNumber}");
            Console.WriteLine($"Имя устройства: {connector.DeviceName}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Описание результата: {connector.ResultDescription}");
            Console.WriteLine($"Признак успешного завершения операции: {connector.IsProcessed}");
            Console.WriteLine($"Модель ККТ: {connector.DeviceModel}");
            Console.WriteLine($"Заводской номер ККТ: {connector.DeviceSerialNumber}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}
