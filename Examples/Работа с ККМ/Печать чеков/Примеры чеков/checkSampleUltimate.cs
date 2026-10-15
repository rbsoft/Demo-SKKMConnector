using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Комплексный чек: объекты расчёта, НДС, агент, маркировка, безнал, нефискальные строки.
/// </summary>
public class checkSampleUltimate : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Комплексный чек: все объекты, НДС, агент, маркировка";

    public async Task<SkkmConnector> PostComplexCheck()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier("Иванов И. И.", "500100732259");
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        connector.TimeZone = CheckTimeZone.MskPlus5;
        connector.SenderEmail = "ivanov@mail.ru";
        connector.SaleAddress = "г. Москва, ул. Ленина, д. 1";
        connector.SaleLocation = "https://shop.ru";
        connector.TextBefore = "Текст 1";
        connector.TextAfter = "Текст 2";

        connector.CustomerInfo = "ООО Ромашка";
        connector.CustomerVatin = "7701234560";
        connector.CustomerEmail = "ivanov@mail.ru";
        connector.CustomerPhone = "+79001234567";
        connector.CustomerDateOfBirth = "01.01.1990";
        connector.CustomerCitizenship = "643";
        connector.CustomerDocumentTypeCode = "21";
        connector.CustomerDocumentData = "4509 123456";
        connector.CustomerAddress = "г. Москва, ул. Ленина, д. 1";

        connector.AgentSign = AgentType.Agent;
        connector.PayingAgentOperation = "Приём платежей";
        connector.PayingAgentPhone = ["+79001234567"];
        connector.ReceivePaymentsOperatorPhone = ["+79001234567"];
        connector.MoneyTransferOperatorPhone = ["+79001234567"];
        connector.MoneyTransferOperatorName = "ООО Рога и копыта";
        connector.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        connector.MoneyTransferOperatorVatin = "7701234560";
        connector.VendorName = "ООО Ромашка";
        connector.VendorVatin = "7701234560";
        connector.VendorPhones = ["+79001234567", "+79007654321"];

        connector.UserAttributeName = "НомерЗаказа";
        connector.UserAttributeValue = "42";
        connector.OperationalAttributeDateTime = "03.09.2026";
        connector.OperationalAttributeOperationId = 42;
        connector.OperationalAttributeData = "Оплата по договору";
        connector.IndustryIdentifierFoiv = "030";
        connector.IndustryAttributeDocumentDate = "21.11.2023";
        connector.IndustryAttributeDocumentNumber = "1944";
        connector.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        connector.AddElectronicPayment(10, ElectronicPaymentMethod.FullPrepayment, "M=0", "Не определено");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.PartialPrepayment, "M=1", "Банковская карта");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.Advance, "M=2", "СБП");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.FullPayment, "RRN=123456789012", "Карта *1234");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.PartialPaymentAndCredit, "M=4", "Перевод с банка");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.CreditTransfer, "M=5", "Эл. кошелёк");
        connector.AddElectronicPayment(10, ElectronicPaymentMethod.CreditPayment, "M=6", "Иной безнал");

        connector.AddText("[center]Комплексный чек: объекты, НДС, агент, маркировка", PrintFont.Big, PrintAlignment.Center);
        connector.AddSeparatorLine(LineStyle.Dotted);

        var line1 = connector.AddPosition("Товар", 2, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.Goods, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line1.Price = 6;
        line1.DiscountSum = 2;
        line1.TaxSum = 0;
        line1.Department = 1;
        line1.ProductCode = "2208 20 290 0";
        line1.AdditionalAttribute = "Доп. реквизит товара";
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line1.PayingAgentOperation = "Приём платежей";
        line1.PayingAgentPhone = ["+79001234567"];
        line1.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line1.MoneyTransferOperatorPhone = ["+79001234567"];
        line1.MoneyTransferOperatorName = "ООО Рога и копыта";
        line1.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line1.MoneyTransferOperatorVatin = "7701234560";
        line1.VendorName = "ООО Ромашка";
        line1.VendorVatin = "7701234560";
        line1.VendorPhones = ["+79001234567", "+79007654321"];

        var line2 = connector.AddPosition("Подакцизный товар", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.ExcisableGoods, signMethodCalculation: SignMethodCalculation.PartialPrepayment);
        line2.Price = 10;
        line2.DiscountSum = 0;
        line2.TaxSum = 1.67m;
        line2.Department = 2;
        line2.ProductCode = "2402 20 900 0";
        line2.AdditionalAttribute = "Подакциз";
        line2.ExciseAmount = 5.5m;
        line2.CountryOfOrigin = "643";
        line2.CustomsDeclaration = "10009100/220211/0001122";
        line2.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line2.AgentSign = AgentType.Agent;
        line2.PayingAgentOperation = "Приём платежей";
        line2.PayingAgentPhone = ["+79001234567"];
        line2.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line2.MoneyTransferOperatorPhone = ["+79001234567"];
        line2.MoneyTransferOperatorName = "ООО Рога и копыта";
        line2.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line2.MoneyTransferOperatorVatin = "7701234560";

        var line3 = connector.AddPosition("Работа", 1, 10.0m, TaxRate.Vat5, signCalculationObject: SignCalculationObject.Work, signMethodCalculation: SignMethodCalculation.Advance);
        line3.Price = 10;
        line3.TaxSum = 0;
        line3.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line3.AgentSign = AgentType.Agent;
        line3.VendorName = "ООО Ромашка";
        line3.VendorVatin = "7701234560";
        line3.VendorPhones = ["+79001234567", "+79007654321"];

        var line4 = connector.AddPosition("Услуга", 1, 10.0m, TaxRate.Vat5_105, signCalculationObject: SignCalculationObject.Service, signMethodCalculation: SignMethodCalculation.FullPayment);
        line4.Price = 10;
        line4.TaxSum = 0;
        line4.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line4.AgentSign = AgentType.Agent;
        line4.PayingAgentOperation = "Приём платежей";
        line4.PayingAgentPhone = ["+79001234567"];
        line4.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line4.MoneyTransferOperatorPhone = ["+79001234567"];
        line4.MoneyTransferOperatorName = "ООО Рога и копыта";
        line4.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line4.MoneyTransferOperatorVatin = "7701234560";
        line4.VendorName = "ООО Ромашка";
        line4.VendorVatin = "7701234560";
        line4.VendorPhones = ["+79001234567", "+79007654321"];

        var line5 = connector.AddPosition("Ставка азартной игры", 1, 10.0m, TaxRate.Vat7, signCalculationObject: SignCalculationObject.GamblingStake, signMethodCalculation: SignMethodCalculation.PartialPaymentAndCredit);
        line5.Price = 10;
        line5.TaxSum = 0;
        line5.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line5.AgentSign = AgentType.Agent;
        var line6 = connector.AddPosition("Выигрыш азартной игры", 1, 10.0m, TaxRate.Vat7_107, signCalculationObject: SignCalculationObject.GamblingPrize, signMethodCalculation: SignMethodCalculation.CreditTransfer);
        line6.Price = 10;
        line6.TaxSum = 0;
        line6.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line6.AgentSign = AgentType.Agent;
        var line7 = connector.AddPosition("Лотерейный билет", 1, 10.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.LotteryTicket, signMethodCalculation: SignMethodCalculation.CreditPayment);
        line7.Price = 10;
        line7.TaxSum = 0;
        line7.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line7.AgentSign = AgentType.Agent;
        var line8 = connector.AddPosition("Выигрыш лотереи", 1, 10.0m, TaxRate.Vat10_110, signCalculationObject: SignCalculationObject.LotteryPrize, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line8.Price = 10;
        line8.TaxSum = 0;
        line8.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line8.AgentSign = AgentType.Agent;
        var line9 = connector.AddPosition("Предоставление РИД", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.IntellectualProperty, signMethodCalculation: SignMethodCalculation.PartialPrepayment);
        line9.Price = 10;
        line9.TaxSum = 0;
        line9.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line9.AgentSign = AgentType.Agent;
        var line10 = connector.AddPosition("Платёж", 1, 10.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.Advance, signMethodCalculation: SignMethodCalculation.Advance);
        line10.Price = 10;
        line10.TaxSum = 0;
        line10.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line10.AgentSign = AgentType.Agent;
        var line11 = connector.AddPosition("Агентское вознаграждение", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.AgentFee, signMethodCalculation: SignMethodCalculation.FullPayment);
        line11.Price = 10;
        line11.TaxSum = 0;
        line11.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line11.AgentSign = AgentType.Agent;
        var line12 = connector.AddPosition("Составной предмет расчёта", 1, 10.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.Payout, signMethodCalculation: SignMethodCalculation.PartialPaymentAndCredit);
        line12.Price = 10;
        line12.TaxSum = 0;
        line12.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line12.AgentSign = AgentType.Agent;
        var line13 = connector.AddPosition("Иной предмет расчёта", 1, 10.0m, TaxRate.Vat22, signCalculationObject: SignCalculationObject.Other, signMethodCalculation: SignMethodCalculation.CreditTransfer);
        line13.Price = 10;
        line13.TaxSum = 0;
        line13.MeasureOfQuantity = MeasureOfQuantity.Other;
        line13.AgentSign = AgentType.Agent;
        var line14 = connector.AddPosition("Имущественное право", 1, 10.0m, TaxRate.Vat22_122, signCalculationObject: SignCalculationObject.PropertyRight, signMethodCalculation: SignMethodCalculation.CreditPayment);
        line14.Price = 10;
        line14.TaxSum = 0;
        line14.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line14.AgentSign = AgentType.Agent;
        var line15 = connector.AddPosition("Внереализационный доход", 1, 10.0m, TaxRate.None, signCalculationObject: SignCalculationObject.NonOperatingIncome, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line15.Price = 10;
        line15.TaxSum = 0;
        line15.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line15.AgentSign = AgentType.Agent;
        var line16 = connector.AddPosition("Страховые взносы", 1, 10.0m, TaxRate.Vat0, signCalculationObject: SignCalculationObject.OtherPayments, signMethodCalculation: SignMethodCalculation.PartialPrepayment);
        line16.Price = 10;
        line16.TaxSum = 0;
        line16.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line16.AgentSign = AgentType.Agent;
        var line17 = connector.AddPosition("Торговый сбор", 1, 10.0m, TaxRate.Vat5, signCalculationObject: SignCalculationObject.TradeFee, signMethodCalculation: SignMethodCalculation.Advance);
        line17.Price = 10;
        line17.TaxSum = 0;
        line17.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line17.AgentSign = AgentType.Agent;
        var line18 = connector.AddPosition("Курортный сбор", 1, 10.0m, TaxRate.Vat5_105, signCalculationObject: SignCalculationObject.TouristTax, signMethodCalculation: SignMethodCalculation.FullPayment);
        line18.Price = 10;
        line18.TaxSum = 0;
        line18.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line18.AgentSign = AgentType.Agent;
        var line19 = connector.AddPosition("Залог", 1, 10.0m, TaxRate.Vat7, signCalculationObject: SignCalculationObject.Deposit, signMethodCalculation: SignMethodCalculation.PartialPaymentAndCredit);
        line19.Price = 10;
        line19.TaxSum = 0;
        line19.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line19.AgentSign = AgentType.Agent;
        var line20 = connector.AddPosition("Расход", 1, 10.0m, TaxRate.Vat7_107, signCalculationObject: SignCalculationObject.Expense, signMethodCalculation: SignMethodCalculation.CreditTransfer);
        line20.Price = 10;
        line20.TaxSum = 0;
        line20.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line20.AgentSign = AgentType.Agent;
        var line21 = connector.AddPosition("Взносы на ОПС ИП", 1, 10.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.PensionContributionIp, signMethodCalculation: SignMethodCalculation.CreditPayment);
        line21.Price = 10;
        line21.TaxSum = 0;
        line21.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line21.AgentSign = AgentType.Agent;
        var line22 = connector.AddPosition("Взносы на ОПС", 1, 10.0m, TaxRate.Vat10_110, signCalculationObject: SignCalculationObject.PensionContribution, signMethodCalculation: SignMethodCalculation.FullPrepayment);
        line22.Price = 10;
        line22.TaxSum = 0;
        line22.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line22.AgentSign = AgentType.Agent;
        var line23 = connector.AddPosition("Взносы на ОМС ИП", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.MedicalContributionIp, signMethodCalculation: SignMethodCalculation.PartialPrepayment);
        line23.Price = 10;
        line23.TaxSum = 0;
        line23.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line23.AgentSign = AgentType.Agent;
        var line24 = connector.AddPosition("Взносы на ОМС", 1, 10.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.MedicalContribution, signMethodCalculation: SignMethodCalculation.Advance);
        line24.Price = 10;
        line24.TaxSum = 0;
        line24.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line24.AgentSign = AgentType.Agent;
        var line25 = connector.AddPosition("Взносы на ОСС", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.SocialContribution, signMethodCalculation: SignMethodCalculation.FullPayment);
        line25.Price = 10;
        line25.TaxSum = 0;
        line25.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line25.AgentSign = AgentType.Agent;
        var line26 = connector.AddPosition("Платёж казино", 1, 10.0m, TaxRate.Vat20_120, signCalculationObject: SignCalculationObject.CasinoPayment, signMethodCalculation: SignMethodCalculation.PartialPaymentAndCredit);
        line26.Price = 10;
        line26.TaxSum = 0;
        line26.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line26.AgentSign = AgentType.Agent;
        var line27 = connector.AddPosition("Выдача денежных средств", 1, 10.0m, TaxRate.Vat22, signCalculationObject: SignCalculationObject.CashWithdrawalByAgent, signMethodCalculation: SignMethodCalculation.CreditTransfer);
        line27.Price = 10;
        line27.TaxSum = 0;
        line27.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line27.AgentSign = AgentType.Agent;

        var line28 = connector.AddPosition("АТНМ", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.АТНМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line28.Price = 10;
        line28.TaxSum = 1.67m;
        line28.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line28.AgentSign = AgentType.Agent;
        line28.ExciseAmount = 12.3m;
        line28.CountryOfOrigin = "276";
        line28.CustomsDeclaration = "10009100/140923/0001122";
        line28.ProductCode = "2208 20 290 0";

        var line29 = connector.AddPosition("АТМ", 1, 10.0m, TaxRate.Vat20, signCalculationObject: SignCalculationObject.АТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line29.Price = 10;
        line29.TaxSum = 1.67m;
        line29.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line29.AgentSign = AgentType.Agent;
        line29.ExciseAmount = 185.4m;
        line29.CountryOfOrigin = "276";
        line29.CustomsDeclaration = "10009100/140923/0001122";
        line29.ProductCode = "2208 20 290 0";
        line29.AdditionalAttribute = "Импорт АТМ";
        line29.Marking = "MDEwNDYwMTkwNzAwMjgwNTIxNUR0RDJReUlCSjVrYR05MUYwMTMdOTJnajR4alVwdzlCZHVLUXZhT3pNYmFyeGhkVFRqWmp1cFR0RkFZOTl0WmhVPQ==";
        line29.GoodCodeGtin = "04601907002805";
        line29.IndustryIdentifierFoiv = "030";
        line29.IndustryAttributeDocumentDate = "21.11.2023";
        line29.IndustryAttributeDocumentNumber = "1944";
        line29.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        var line30 = connector.AddPosition("ТНМ", 1, 10.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.ТНМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line30.Price = 10;
        line30.TaxSum = 0;
        line30.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line30.AgentSign = AgentType.Agent;
        line30.ProductCode = "0402 10 190 0";

        var line31 = connector.AddPosition("ТМ", 1, 10.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.ТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line31.Price = 10;
        line31.TaxSum = 0.91m;
        line31.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line31.AgentSign = AgentType.Agent;
        line31.Marking = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        line31.FractionalQuantityNumerator = 1;
        line31.FractionalQuantityDenominator = 4;
        line31.IndustryIdentifierFoiv = "030";
        line31.IndustryAttributeDocumentDate = "21.11.2023";
        line31.IndustryAttributeDocumentNumber = "1944";
        line31.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        var line32 = connector.AddPosition("Сыр", 0.5m, 10.0m, TaxRate.Vat10, signCalculationObject: SignCalculationObject.ТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line32.Price = 20;
        line32.TaxSum = 0.91m;
        line32.MeasureOfQuantity = MeasureOfQuantity.Kilogram;
        line32.AgentSign = AgentType.Agent;
        line32.Marking = "MDEwNDYwMjIyMDAwNjU0OTIxNW9wRmNtSx05M2RHVno=";
        line32.IndustryIdentifierFoiv = "030";
        line32.IndustryAttributeDocumentDate = "21.11.2023";
        line32.IndustryAttributeDocumentNumber = "1944";
        line32.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        connector.AddBarcode(BarcodeType.CODE128, "ABC-12345", PrintAlignment.Center);
        connector.AddBarcode(BarcodeType.QR, "https://shop.ru", PrintAlignment.Center);
        connector.AddPicture(filePath, PictureAlignment.Right);
        connector.AddSeparatorLine(LineStyle.Dotted);

        connector.Cash = 50;
        connector.ElectronicPayment = 70;
        connector.AdvancePayment = 70;
        connector.Credit = 70;
        connector.CashProvision = 60;

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
