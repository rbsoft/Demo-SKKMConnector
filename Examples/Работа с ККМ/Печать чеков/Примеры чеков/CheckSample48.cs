using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample48 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Данные агента в позиции";

    public async Task<SkkmConnector> PostCheckSample48()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Вода 1л.", 1m, 90m, 90.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        line1.SignSubjectCalculationAgent = AgentType.Agent;
        line1.VendorDataName = "ООО Ромашка";
        line1.VendorDataPhones = new[] { "+79001234567" };
        line1.VendorDataVatin = "7701234560";
        connector.Cash = 90;
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
