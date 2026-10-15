using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckMarking : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Печать чека с маркировкой";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostPrintCheckMarking()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);

        connector.NewCheck(CheckType.Sale);

        var line = connector.AddPosition("Тестовый маркированный товар", 1, 900, TaxRate.Vat20,
            SignCalculationObject.ТМ, SignMethodCalculation.FullPayment);
        line.Marking = "MDEwNDYyOTMwODg3NzA0NDIxRHprY1l0Mh04MDA1MDkwMDAwHTkzZEdWeg==";

        connector.Cash = 900;

        await connector.PrintCheck();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
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
