using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample58 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Два одинаковых кода маркировки в двух позициях";

    public async Task<SkkmConnector> PostCheckSample58()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Сыр", 0.2m, 300m, 60.0m, TaxRate.Vat10, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Kilogram);
        line1.TaxSum = 0m;
        line1.Cis_Base64 = "MDEwNDYwMjIyMDAwNjU0OTIxNW9wRmNtSx05M2RHVno=";
        var line2 = connector.AddPosition("Сыр", 0.15m, 300m, 45.0m, TaxRate.Vat10, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Kilogram);
        line2.TaxSum = 0m;
        line2.Cis_Base64 = "MDEwNDYwMjIyMDAwNjU0OTIxNW9wRmNtSx05M2RHVno=";
        connector.Cash = 105.0m;
        if (await connector.PrintCheck())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"ФП чека: {connector.FiscalSign}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
