using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample35 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все признаки предмета расчета";

    public async Task<SkkmConnector> PostCheckSample35()
    {
        connector.DeviceName = deviceName;
        connector.CashierName = cashierName;
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        var line1 = connector.AddPosition("Товар", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line1.TaxSum = 0m;
        var line2 = connector.AddPosition("Подакцизный товар", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.ExcisableGoods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line2.TaxSum = 0m;
        var line3 = connector.AddPosition("Работа", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Work, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line3.TaxSum = 0m;
        var line4 = connector.AddPosition("Услуга", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Service, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line4.TaxSum = 0m;
        var line5 = connector.AddPosition("Ставка азартной игры", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.GamblingStake, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line5.TaxSum = 0m;
        var line6 = connector.AddPosition("Выигрыш азартной игры", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.GamblingPrize, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line6.TaxSum = 0m;
        var line7 = connector.AddPosition("Лотерейный билет", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.LotteryTicket, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line7.TaxSum = 0m;
        var line8 = connector.AddPosition("Выигрыш лотереи", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.LotteryPrize, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line8.TaxSum = 0m;
        var line9 = connector.AddPosition("Предоставление РИД", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.IntellectualProperty, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line9.TaxSum = 0m;
        var line10 = connector.AddPosition("Платёж", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Advance, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line10.TaxSum = 0m;
        var line11 = connector.AddPosition("Агентское вознаграждение", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.AgentFee, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line11.TaxSum = 0m;
        var line12 = connector.AddPosition("Составной предмет расчёта", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Payout, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line12.TaxSum = 0m;
        var line13 = connector.AddPosition("Иной предмет расчёта", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Other, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line13.TaxSum = 0m;
        var line14 = connector.AddPosition("Имущественное право", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.PropertyRight, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line14.TaxSum = 0m;
        var line15 = connector.AddPosition("Внереализационный доход", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.NonOperatingIncome, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line15.TaxSum = 0m;
        var line16 = connector.AddPosition("Страховые взносы", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.OtherPayments, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line16.TaxSum = 0m;
        var line17 = connector.AddPosition("Торговый сбор", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.TradeFee, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line17.TaxSum = 0m;
        var line18 = connector.AddPosition("Курортный сбор", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.TouristTax, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line18.TaxSum = 0m;
        var line19 = connector.AddPosition("Залог", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Deposit, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line19.TaxSum = 0m;
        var line20 = connector.AddPosition("Расход", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.Expense, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line20.TaxSum = 0m;
        var line21 = connector.AddPosition("Взносы на ОПС ИП", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.PensionContributionIp, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line21.TaxSum = 0m;
        var line22 = connector.AddPosition("Взносы на ОПС", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.PensionContribution, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line22.TaxSum = 0m;
        var line23 = connector.AddPosition("Взносы на ОМС ИП", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.MedicalContributionIp, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line23.TaxSum = 0m;
        var line24 = connector.AddPosition("Взносы на ОМС", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.MedicalContribution, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line24.TaxSum = 0m;
        var line25 = connector.AddPosition("Взносы на ОСС", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.SocialContribution, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line25.TaxSum = 0m;
        var line26 = connector.AddPosition("Платёж казино", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.CasinoPayment, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line26.TaxSum = 0m;
        var line27 = connector.AddPosition("Выдача денежных средств", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.CashWithdrawalByAgent, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line27.TaxSum = 0m;
        var line28 = connector.AddPosition("АТНМ", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.ExcisableMarkedWithoutCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line28.TaxSum = 0m;
        var line29 = connector.AddPosition("АТМ", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.ExcisableMarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line29.TaxSum = 0m;
        var line30 = connector.AddPosition("ТНМ", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.MarkedWithoutCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line30.TaxSum = 0m;
        var line31 = connector.AddPosition("ТМ", 1m, 1m, 1.0m, TaxRate.None, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line31.TaxSum = 0m;
        connector.Cash = 31.0m;
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
