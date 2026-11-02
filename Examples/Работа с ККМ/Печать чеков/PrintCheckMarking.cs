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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        var line = connector.AddPosition("Тестовый маркированный товар", 1, 900, 900, TaxRate.Vat20,
            SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment);
        line.Cis_Base64 = "MDEwNDYyOTMwODg3NzA0NDIxRHprY1l0Mh04MDA1MDkwMDAwHTkzZEdWeg==";

        connector.Cash = 900;

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
