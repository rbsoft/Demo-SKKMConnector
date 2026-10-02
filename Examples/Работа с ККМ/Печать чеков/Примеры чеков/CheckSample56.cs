using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample56 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "512 некорректный код маркировки - Код маркировки заблокирован по постановлению";

    public async Task<SkkmConnector> PostCheckSample56()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Сыр", 0.353m, 70.6m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.ТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 200m;
        line1.TaxSum = 0m;
        line1.MeasurementUnit = "11";
        line1.MeasureOfQuantity = MeasureOfQuantity.Kilogram;
        line1.Marking = "MDEwNDYwMjIyMDAwNjU0OTIxNW9wRmNtSx05M2RHVno=";
        connector.Cash = 70.6m;
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
