using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopy : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия чека по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostPrintCheckCopy()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.DocumentId = documentId;
        await connector.PrintCheckCopy();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер чека за смену: {connector.CheckNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}\n" +
                $"Время ККТ: {connector.DeviceDateTime}\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}