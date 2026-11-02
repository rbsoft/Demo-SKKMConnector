using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample47 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все способы электронной оплаты (0-6)";

    public async Task<SkkmConnector> PostCheckSample47()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        // По одной оплате на каждый способ безналичной оплаты:
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.FullPrepayment, "M=0", "Способ 0");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.PartialPrepayment, "M=1", "Способ 1");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.Advance, "M=2", "Способ 2");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.FullPayment, "M=3", "Способ 3");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.PartialPaymentAndCredit, "M=4", "Способ 4");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.CreditTransfer, "M=5", "Способ 5");
        connector.AddElectronicPaymentInfo(10m, ElectronicPaymentMethod.CreditPayment, "M=6", "Способ 6");

        var line1 = connector.AddPosition("Вода 1л.", 1m, 70m, 70.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        connector.ElectronicPayment = 70m;
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
