using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopyById : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия чека по идентификатору";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostPrintCheckCopyById()
    {
        connector.DeviceName = deviceName;
        connector.DocumentId = documentId;
        await connector.PrintCheckCopy();
        if (connector.Ok)
            Console.WriteLine(
                "Копия чека напечатана.\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
