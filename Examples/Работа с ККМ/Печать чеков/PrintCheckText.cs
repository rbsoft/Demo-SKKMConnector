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
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;

        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition("Кофе американо", 1, 150, 150, TaxRate.Vat20,
            SignCalculationObject.Goods, SignMethodCalculation.FullPayment);

        connector.AddSeparatorLine(LineStyle.Dashed);
        connector.AddText("Спасибо за покупку!");
        connector.AddBarcode(BarcodeType.EAN13, "4600000000001");

        connector.Cash = 150;

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
