using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample59 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Указание отраслевого реквизита";

    public async Task<SkkmConnector> PostCheckSample59()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Молоко 1л.", 1m, 101m, 101.0m, TaxRate.Vat0, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        line1.Cis_Base64 = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        line1.IndustryAttributeIdentifierFoiv = "030";
        line1.IndustryAttributeDocumentDate = "21.11.2023";
        line1.IndustryAttributeDocumentNumber = "1944";
        line1.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";
        connector.Cash = 101.0m;
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
