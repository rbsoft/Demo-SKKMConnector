using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetFiscalization : Sample
{
    public const string GroupPath = "Работа с ККМ|Фискализация";
    public const string Title = "Результат фискализации";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> GetGetFiscalization()
    {
        connector.DeviceName = deviceName;
        await connector.GetFiscalization(documentId);
        if (connector.Ok)
            Console.WriteLine(
                "Результат фискализации получен.\n" +
                $"docId: {connector.DocumentId}\n" +
                $"Тип операции: {connector.FiscalizationResultOperationType}\n" +
                $"РНМ: {connector.RnNumber}\n" +
                $"ИНН организации: {connector.FiscalizationResultVatin}\n" +
                $"Организация: {connector.FiscalizationResultCompanyName}\n" +
                $"СНО (коды): {connector.FiscalizationResultTaxationSystems}\n" +
                $"Версия ФФД ККТ: {connector.FiscalizationResultFfdVersionKkt}, ФН: {connector.FiscalizationResultFfdVersionFn}\n" +
                $"Фискальный режим: {connector.IsFiscal}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФД: {connector.CheckNumber}\n" +
                $"Фискальный признак: {connector.FiscalSign}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
