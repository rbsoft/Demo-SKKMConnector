using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckText : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Печать чека с текстом";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> PostPrintCheckText()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);

        connector.NewCheck(CheckType.Sale);

        connector.AddPosition("Кофе американо", 1, 150, TaxRate.Vat20,
            SignCalculationObject.Goods, SignMethodCalculation.FullPayment);

        connector.AddSeparatorLine(LineStyle.Dashed);
        connector.AddText("Спасибо за покупку!");
        connector.AddBarcode(BarcodeType.EAN13, "4600000000001");

        connector.Cash = 150;

        await connector.PrintCheck();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
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
