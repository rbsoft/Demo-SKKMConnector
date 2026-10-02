using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample35 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Все признаки предмета расчета";

    public async Task<SkkmConnector> PostCheckSample35()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Товар", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 1m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line2 = connector.AddPosition("Подакцизный товар", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.ExcisableGoods, signMethodCalculation: SignMethodCalculation.FullPayment);
        line2.Price = 1m;
        line2.TaxSum = 0m;
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line3 = connector.AddPosition("Работа", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Work, signMethodCalculation: SignMethodCalculation.FullPayment);
        line3.Price = 1m;
        line3.TaxSum = 0m;
        line3.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line4 = connector.AddPosition("Услуга", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 1m;
        line4.TaxSum = 0m;
        line4.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line5 = connector.AddPosition("Ставка азартной игры", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.GamblingStake, signMethodCalculation: SignMethodCalculation.FullPayment);
        line5.Price = 1m;
        line5.TaxSum = 0m;
        line5.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line6 = connector.AddPosition("Выигрыш азартной игры", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.GamblingPrize, signMethodCalculation: SignMethodCalculation.FullPayment);
        line6.Price = 1m;
        line6.TaxSum = 0m;
        line6.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line7 = connector.AddPosition("Лотерейный билет", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.LotteryTicket, signMethodCalculation: SignMethodCalculation.FullPayment);
        line7.Price = 1m;
        line7.TaxSum = 0m;
        line7.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line8 = connector.AddPosition("Выигрыш лотереи", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.LotteryPrize, signMethodCalculation: SignMethodCalculation.FullPayment);
        line8.Price = 1m;
        line8.TaxSum = 0m;
        line8.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line9 = connector.AddPosition("Предоставление РИД", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.IntellectualProperty, signMethodCalculation: SignMethodCalculation.FullPayment);
        line9.Price = 1m;
        line9.TaxSum = 0m;
        line9.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line10 = connector.AddPosition("Платёж", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Advance, signMethodCalculation: SignMethodCalculation.FullPayment);
        line10.Price = 1m;
        line10.TaxSum = 0m;
        line10.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line11 = connector.AddPosition("Агентское вознаграждение", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.AgentFee, signMethodCalculation: SignMethodCalculation.FullPayment);
        line11.Price = 1m;
        line11.TaxSum = 0m;
        line11.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line12 = connector.AddPosition("Составной предмет расчёта", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Payout, signMethodCalculation: SignMethodCalculation.FullPayment);
        line12.Price = 1m;
        line12.TaxSum = 0m;
        line12.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line13 = connector.AddPosition("Иной предмет расчёта", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Other, signMethodCalculation: SignMethodCalculation.FullPayment);
        line13.Price = 1m;
        line13.TaxSum = 0m;
        line13.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line14 = connector.AddPosition("Имущественное право", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.PropertyRight, signMethodCalculation: SignMethodCalculation.FullPayment);
        line14.Price = 1m;
        line14.TaxSum = 0m;
        line14.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line15 = connector.AddPosition("Внереализационный доход", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.NonOperatingIncome, signMethodCalculation: SignMethodCalculation.FullPayment);
        line15.Price = 1m;
        line15.TaxSum = 0m;
        line15.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line16 = connector.AddPosition("Страховые взносы", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.OtherPayments, signMethodCalculation: SignMethodCalculation.FullPayment);
        line16.Price = 1m;
        line16.TaxSum = 0m;
        line16.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line17 = connector.AddPosition("Торговый сбор", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.TradeFee, signMethodCalculation: SignMethodCalculation.FullPayment);
        line17.Price = 1m;
        line17.TaxSum = 0m;
        line17.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line18 = connector.AddPosition("Курортный сбор", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.TouristTax, signMethodCalculation: SignMethodCalculation.FullPayment);
        line18.Price = 1m;
        line18.TaxSum = 0m;
        line18.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line19 = connector.AddPosition("Залог", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Deposit, signMethodCalculation: SignMethodCalculation.FullPayment);
        line19.Price = 1m;
        line19.TaxSum = 0m;
        line19.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line20 = connector.AddPosition("Расход", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Expense, signMethodCalculation: SignMethodCalculation.FullPayment);
        line20.Price = 1m;
        line20.TaxSum = 0m;
        line20.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line21 = connector.AddPosition("Взносы на ОПС ИП", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.PensionContributionIp, signMethodCalculation: SignMethodCalculation.FullPayment);
        line21.Price = 1m;
        line21.TaxSum = 0m;
        line21.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line22 = connector.AddPosition("Взносы на ОПС", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.PensionContribution, signMethodCalculation: SignMethodCalculation.FullPayment);
        line22.Price = 1m;
        line22.TaxSum = 0m;
        line22.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line23 = connector.AddPosition("Взносы на ОМС ИП", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.MedicalContributionIp, signMethodCalculation: SignMethodCalculation.FullPayment);
        line23.Price = 1m;
        line23.TaxSum = 0m;
        line23.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line24 = connector.AddPosition("Взносы на ОМС", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.MedicalContribution, signMethodCalculation: SignMethodCalculation.FullPayment);
        line24.Price = 1m;
        line24.TaxSum = 0m;
        line24.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line25 = connector.AddPosition("Взносы на ОСС", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.SocialContribution, signMethodCalculation: SignMethodCalculation.FullPayment);
        line25.Price = 1m;
        line25.TaxSum = 0m;
        line25.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line26 = connector.AddPosition("Платёж казино", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.CasinoPayment, signMethodCalculation: SignMethodCalculation.FullPayment);
        line26.Price = 1m;
        line26.TaxSum = 0m;
        line26.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line27 = connector.AddPosition("Выдача денежных средств", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.CashWithdrawalByAgent, signMethodCalculation: SignMethodCalculation.FullPayment);
        line27.Price = 1m;
        line27.TaxSum = 0m;
        line27.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line28 = connector.AddPosition("АТНМ", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.АТНМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line28.Price = 1m;
        line28.TaxSum = 0m;
        line28.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line29 = connector.AddPosition("АТМ", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.АТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line29.Price = 1m;
        line29.TaxSum = 0m;
        line29.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line30 = connector.AddPosition("ТНМ", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.ТНМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line30.Price = 1m;
        line30.TaxSum = 0m;
        line30.MeasureOfQuantity = MeasureOfQuantity.Piece;
        var line31 = connector.AddPosition("ТМ", 1m, 1.0m, TaxRate.None, signCalculationObject: SignCalculationObject.ТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line31.Price = 1m;
        line31.TaxSum = 0m;
        line31.MeasureOfQuantity = MeasureOfQuantity.Piece;
        connector.Cash = 31.0m;
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
