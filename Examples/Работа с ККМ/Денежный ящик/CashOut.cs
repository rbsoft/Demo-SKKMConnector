using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CashOut : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Выемка";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> PostCashOut()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.CashAmount = 500;
        await connector.CashOut();

        if (connector.Ok)
            Console.WriteLine(
                "Выемка выполнена.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер ФД за смену: {connector.CheckDocNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Сумма выемки: {connector.CheckSum}\n" +
                $"Остаток наличных: {connector.CheckCashSum}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"РНМ: {connector.RnNumber}, ФН: {connector.FnNumber}\n" +
                $"ОФД: {connector.OfdName}, ФНС: {connector.FnsUrl}\n" +
                $"Модель ККТ: {connector.DeviceModel}, серийный: {connector.DeviceSerialNumber}, прошивка: {connector.DeviceFirmwareVersion}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}