using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Fiscalization : Sample
{
    public const string GroupPath = "Работа с ККМ|Фискализация";
    public const string Title = "Фискализация кассы";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostFiscalization()
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
        await connector.Fiscalization();
        if (connector.Ok)
            Console.WriteLine(
                "Фискализация выполнена.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата по часам ФН: {connector.FiscalDateTime}\n" +
                $"Фискальный режим: {connector.IsFiscal}\n" +
                $"Модель ККТ: {connector.DeviceModel}\n" +
                $"Заводской номер ККТ: {connector.DeviceSerialNumber}\n" +
                $"Версия прошивки: {connector.DeviceFirmwareVersion}\n" +
                $"Версия ФФД: {connector.DeviceFfdVersion}\n" +
                $"Версия ФФД ФН: {connector.FnFfdVersion}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
