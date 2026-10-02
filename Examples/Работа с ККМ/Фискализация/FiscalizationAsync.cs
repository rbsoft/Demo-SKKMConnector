using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class FiscalizationAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Фискализация";
    public const string Title = "Асинхронная фискализация";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostFiscalizationAsync()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.FiscalizationRnNumber = "00031415926";
        connector.FiscalizationFn = "0123123123123";
        connector.FiscalizationCompanyName = "ООО 'Ромашка'";
        connector.FiscalizationVatin = "7722345678";
        connector.FiscalizationTaxationSystems =
            [TaxSystem.Osn, TaxSystem.Usn, TaxSystem.UsnDr, TaxSystem.Esn, TaxSystem.Psn];
        connector.FiscalizationFfdVersionKkt = "1.2";
        connector.FiscalizationFfdVersionFn = "1.2";
        connector.FiscalizationIsEncrypted = false;
        connector.FiscalizationIsOffline = false;
        connector.FiscalizationIsBsoSign = true;
        connector.FiscalizationRegistrationLabelCodes = "3.1";
        connector.FiscalizationReasonCode = FiscalizationReasonCode.RequisitesChange;
        connector.FiscalizationOfdName = "Тестовый ОФД";
        connector.FiscalizationOfdVatin = "1234554321";
        connector.FiscalizationFnsUrl = "nalog.ru";
        connector.FiscalizationSenderEmail = "ivanov@mail.ru";
        connector.FiscalizationIsMarking = true;
        connector.FiscalizationIsExcisable = true;
        await connector.FiscalizationAsync();
        if (connector.Ok)
            Console.WriteLine($"Фискализация поставлена в очередь. Идентификатор документа (docId): {connector.DocumentId}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
