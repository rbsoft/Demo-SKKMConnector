using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OpenCashdrawer : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Открыть ящик";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostOpenCashdrawer()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        await connector.OpenCashdrawer();

        if (connector.Ok)
            Console.WriteLine(
                "Денежный ящик открыт.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Номер денежного ящика: {connector.CheckDrawerNumber}\n" +
                $"Номер фискального документа (ФД): {connector.CheckDocNumber}\n" +
                $"Номер ФД за смену: {connector.CheckDocNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"Адрес расчётов: {connector.CheckSaleAddress}\n" +
                $"Место расчётов: {connector.CheckSaleLocation}\n" +
                $"Тип документа: {connector.CheckTaskType}\n" +
                $"Имя кассы: {connector.CheckDeviceName}\n" +
                $"Обработан успешно: {connector.CheckIsProcessed}\n" +
                $"Версия сервера ККМ: {connector.CheckServerVersion}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                $"Организация: {connector.OrganizationInfo}, ИНН: {connector.OrganizationVatin}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"ОФД: {connector.OfdName}, ИНН ОФД: {connector.OfdVatin}\n" +
                $"Устройство: {connector.DeviceModel} (сер. {connector.DeviceSerialNumber}, прошивка {connector.DeviceFirmwareVersion}, " +
                $"ФФД {connector.DeviceFfdVersion}, ФФД ФН {connector.FnFfdVersion}, тип {connector.DeviceClass})\n" +
                $"Фискальный режим: {connector.IsFiscal}\n" +
                $"Ширина чековой ленты: {connector.LineLength} симв. / {connector.LineLengthPixels} пикс.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
