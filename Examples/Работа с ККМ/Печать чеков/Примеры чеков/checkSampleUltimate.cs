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
        connector.CashierName = "Иванов И. И.";
        connector.CashierVatin = "500100732259";
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        connector.TimeZone = CheckTimeZone.MskPlus5;
        connector.SenderEmail = "ivanov@mail.ru";
        connector.SaleAddress = "г. Москва, ул. Ленина, д. 1";
        connector.SaleLocation = "Торговый зал";
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
        connector.VendorDataName = "ООО Ромашка";
        connector.VendorDataVatin = "7701234560";
        connector.VendorDataPhones = ["+79001234567", "+79007654321"];

        connector.UserAttributeName = "НомерЗаказа";
        connector.UserAttributeValue = "42";
        connector.OperationalAttributeDateTime = "03.09.2026";
        connector.OperationalAttributeOperationID = 42;
        connector.OperationalAttributeOperationData = "Оплата по договору";
        connector.IndustryAttributeIdentifierFoiv = "030";
        connector.IndustryAttributeDocumentDate = "21.11.2023";
        connector.IndustryAttributeDocumentNumber = "1944";
        connector.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.FullPrepayment, "M=0", "Не определено");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.PartialPrepayment, "M=1", "Банковская карта");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.Advance, "M=2", "СБП");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.FullPayment, "RRN=123456789012", "Карта *1234");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.PartialPaymentAndCredit, "M=4", "Перевод с банка");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.CreditTransfer, "M=5", "Эл. кошелёк");
        connector.AddElectronicPaymentInfo(10, ElectronicPaymentMethod.CreditPayment, "M=6", "Иной безнал");

        connector.AddText("[center]Комплексный чек: объекты, НДС, агент, маркировка", PrintFont.Big, PrintAlignment.Center);
        connector.AddSeparatorLine(LineStyle.Dotted);

        var line1 = connector.AddPosition("Товар", 2, 6, 10.0m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPrepayment, MeasureOfQuantity.Piece);
        line1.DiscountSum = 2;
        line1.TaxSum = 0;
        line1.Department = 1;
        line1.AdditionalAttribute = "Доп. реквизит товара";
        line1.PayingAgentOperation = "Приём платежей";
        line1.PayingAgentPhone = ["+79001234567"];
        line1.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line1.MoneyTransferOperatorPhone = ["+79001234567"];
        line1.MoneyTransferOperatorName = "ООО Рога и копыта";
        line1.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line1.MoneyTransferOperatorVATIN = "7701234560";
        line1.VendorDataName = "ООО Ромашка";
        line1.VendorDataVatin = "7701234560";
        line1.VendorDataPhones = ["+79001234567", "+79007654321"];

        var line2 = connector.AddPosition("Подакцизный товар", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.ExcisableGoods, SignMethodCalculation.PartialPrepayment, MeasureOfQuantity.Piece);
        line2.DiscountSum = 0;
        line2.TaxSum = 1.67m;
        line2.Department = 2;
        line2.AdditionalAttribute = "Подакциз";
        line2.ExciseAmount = 5.5m;
        line2.CountryOfOrigin = "643";
        line2.CustomsDeclaration = "10009100/220211/0001122";
        line2.SignSubjectCalculationAgent = AgentType.Agent;
        line2.PayingAgentOperation = "Приём платежей";
        line2.PayingAgentPhone = ["+79001234567"];
        line2.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line2.MoneyTransferOperatorPhone = ["+79001234567"];
        line2.MoneyTransferOperatorName = "ООО Рога и копыта";
        line2.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line2.MoneyTransferOperatorVATIN = "7701234560";

        var line3 = connector.AddPosition("Работа", 1, 10, 10.0m, TaxRate.Vat5, SignCalculationObject.Work, SignMethodCalculation.Advance, MeasureOfQuantity.Piece);
        line3.TaxSum = 0;
        line3.SignSubjectCalculationAgent = AgentType.Agent;
        line3.VendorDataName = "ООО Ромашка";
        line3.VendorDataVatin = "7701234560";
        line3.VendorDataPhones = ["+79001234567", "+79007654321"];

        var line4 = connector.AddPosition("Услуга", 1, 10, 10.0m, TaxRate.Vat5_105, SignCalculationObject.Service, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line4.TaxSum = 0;
        line4.SignSubjectCalculationAgent = AgentType.Agent;
        line4.PayingAgentOperation = "Приём платежей";
        line4.PayingAgentPhone = ["+79001234567"];
        line4.ReceivePaymentsOperatorPhone = ["+79001234567"];
        line4.MoneyTransferOperatorPhone = ["+79001234567"];
        line4.MoneyTransferOperatorName = "ООО Рога и копыта";
        line4.MoneyTransferOperatorAddress = "г. Москва, ул. Ленина, д. 1";
        line4.MoneyTransferOperatorVATIN = "7701234560";
        line4.VendorDataName = "ООО Ромашка";
        line4.VendorDataVatin = "7701234560";
        line4.VendorDataPhones = ["+79001234567", "+79007654321"];

        var line5 = connector.AddPosition("Ставка азартной игры", 1, 10, 10.0m, TaxRate.Vat7, SignCalculationObject.GamblingStake, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line5.TaxSum = 0;
        line5.SignSubjectCalculationAgent = AgentType.Agent;
        var line6 = connector.AddPosition("Выигрыш азартной игры", 1, 10, 10.0m, TaxRate.Vat7_107, SignCalculationObject.GamblingPrize, SignMethodCalculation.CreditTransfer, MeasureOfQuantity.Piece);
        line6.TaxSum = 0;
        line6.SignSubjectCalculationAgent = AgentType.Agent;
        var line7 = connector.AddPosition("Лотерейный билет", 1, 10, 10.0m, TaxRate.Vat10, SignCalculationObject.LotteryTicket, SignMethodCalculation.CreditPayment, MeasureOfQuantity.Piece);
        line7.TaxSum = 0;
        line7.SignSubjectCalculationAgent = AgentType.Agent;
        var line8 = connector.AddPosition("Выигрыш лотереи", 1, 10, 10.0m, TaxRate.Vat10_110, SignCalculationObject.LotteryPrize, SignMethodCalculation.FullPrepayment, MeasureOfQuantity.Piece);
        line8.TaxSum = 0;
        line8.SignSubjectCalculationAgent = AgentType.Agent;
        var line9 = connector.AddPosition("Предоставление РИД", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.IntellectualProperty, SignMethodCalculation.PartialPrepayment, MeasureOfQuantity.Piece);
        line9.TaxSum = 0;
        line9.SignSubjectCalculationAgent = AgentType.Agent;
        var line10 = connector.AddPosition("Платёж", 1, 10, 10.0m, TaxRate.Vat20_120, SignCalculationObject.Advance, SignMethodCalculation.Advance, MeasureOfQuantity.Piece);
        line10.TaxSum = 0;
        line10.SignSubjectCalculationAgent = AgentType.Agent;
        var line11 = connector.AddPosition("Агентское вознаграждение", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.AgentFee, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line11.TaxSum = 0;
        line11.SignSubjectCalculationAgent = AgentType.Agent;
        var line12 = connector.AddPosition("Составной предмет расчёта", 1, 10, 10.0m, TaxRate.Vat20_120, SignCalculationObject.Payout, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line12.TaxSum = 0;
        line12.SignSubjectCalculationAgent = AgentType.Agent;
        var line13 = connector.AddPosition("Иной предмет расчёта", 1, 10, 10.0m, TaxRate.Vat22, SignCalculationObject.Other, SignMethodCalculation.CreditTransfer, MeasureOfQuantity.Other);
        line13.TaxSum = 0;
        line13.SignSubjectCalculationAgent = AgentType.Agent;
        var line14 = connector.AddPosition("Имущественное право", 1, 10, 10.0m, TaxRate.Vat22_122, SignCalculationObject.PropertyRight, SignMethodCalculation.CreditPayment, MeasureOfQuantity.Piece);
        line14.TaxSum = 0;
        line14.SignSubjectCalculationAgent = AgentType.Agent;
        var line15 = connector.AddPosition("Внереализационный доход", 1, 10, 10.0m, TaxRate.None, SignCalculationObject.NonOperatingIncome, SignMethodCalculation.FullPrepayment, MeasureOfQuantity.Piece);
        line15.TaxSum = 0;
        line15.SignSubjectCalculationAgent = AgentType.Agent;
        var line16 = connector.AddPosition("Страховые взносы", 1, 10, 10.0m, TaxRate.Vat0, SignCalculationObject.OtherPayments, SignMethodCalculation.PartialPrepayment, MeasureOfQuantity.Piece);
        line16.TaxSum = 0;
        line16.SignSubjectCalculationAgent = AgentType.Agent;
        var line17 = connector.AddPosition("Торговый сбор", 1, 10, 10.0m, TaxRate.Vat5, SignCalculationObject.TradeFee, SignMethodCalculation.Advance, MeasureOfQuantity.Piece);
        line17.TaxSum = 0;
        line17.SignSubjectCalculationAgent = AgentType.Agent;
        var line18 = connector.AddPosition("Курортный сбор", 1, 10, 10.0m, TaxRate.Vat5_105, SignCalculationObject.TouristTax, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line18.TaxSum = 0;
        line18.SignSubjectCalculationAgent = AgentType.Agent;
        var line19 = connector.AddPosition("Залог", 1, 10, 10.0m, TaxRate.Vat7, SignCalculationObject.Deposit, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line19.TaxSum = 0;
        line19.SignSubjectCalculationAgent = AgentType.Agent;
        var line20 = connector.AddPosition("Расход", 1, 10, 10.0m, TaxRate.Vat7_107, SignCalculationObject.Expense, SignMethodCalculation.CreditTransfer, MeasureOfQuantity.Piece);
        line20.TaxSum = 0;
        line20.SignSubjectCalculationAgent = AgentType.Agent;
        var line21 = connector.AddPosition("Взносы на ОПС ИП", 1, 10, 10.0m, TaxRate.Vat10, SignCalculationObject.PensionContributionIp, SignMethodCalculation.CreditPayment, MeasureOfQuantity.Piece);
        line21.TaxSum = 0;
        line21.SignSubjectCalculationAgent = AgentType.Agent;
        var line22 = connector.AddPosition("Взносы на ОПС", 1, 10, 10.0m, TaxRate.Vat10_110, SignCalculationObject.PensionContribution, SignMethodCalculation.FullPrepayment, MeasureOfQuantity.Piece);
        line22.TaxSum = 0;
        line22.SignSubjectCalculationAgent = AgentType.Agent;
        var line23 = connector.AddPosition("Взносы на ОМС ИП", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.MedicalContributionIp, SignMethodCalculation.PartialPrepayment, MeasureOfQuantity.Piece);
        line23.TaxSum = 0;
        line23.SignSubjectCalculationAgent = AgentType.Agent;
        var line24 = connector.AddPosition("Взносы на ОМС", 1, 10, 10.0m, TaxRate.Vat20_120, SignCalculationObject.MedicalContribution, SignMethodCalculation.Advance, MeasureOfQuantity.Piece);
        line24.TaxSum = 0;
        line24.SignSubjectCalculationAgent = AgentType.Agent;
        var line25 = connector.AddPosition("Взносы на ОСС", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.SocialContribution, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line25.TaxSum = 0;
        line25.SignSubjectCalculationAgent = AgentType.Agent;
        var line26 = connector.AddPosition("Платёж казино", 1, 10, 10.0m, TaxRate.Vat20_120, SignCalculationObject.CasinoPayment, SignMethodCalculation.PartialPaymentAndCredit, MeasureOfQuantity.Piece);
        line26.TaxSum = 0;
        line26.SignSubjectCalculationAgent = AgentType.Agent;
        var line27 = connector.AddPosition("Выдача денежных средств", 1, 10, 10.0m, TaxRate.Vat22, SignCalculationObject.CashWithdrawalByAgent, SignMethodCalculation.CreditTransfer, MeasureOfQuantity.Piece);
        line27.TaxSum = 0;
        line27.SignSubjectCalculationAgent = AgentType.Agent;

        var line28 = connector.AddPosition("АТНМ", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.ExcisableMarkedWithoutCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line28.TaxSum = 1.67m;
        line28.SignSubjectCalculationAgent = AgentType.Agent;
        line28.ExciseAmount = 12.3m;
        line28.CountryOfOrigin = "276";
        line28.CustomsDeclaration = "10009100/140923/0001122";

        var line29 = connector.AddPosition("АТМ", 1, 10, 10.0m, TaxRate.Vat20, SignCalculationObject.ExcisableMarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line29.TaxSum = 1.67m;
        line29.SignSubjectCalculationAgent = AgentType.Agent;
        line29.ExciseAmount = 185.4m;
        line29.CountryOfOrigin = "276";
        line29.CustomsDeclaration = "10009100/140923/0001122";
        line29.AdditionalAttribute = "Импорт АТМ";
        line29.Cis_Base64 = "MDEwNDYwMTkwNzAwMjgwNTIxNUR0RDJReUlCSjVrYR05MUYwMTMdOTJnajR4alVwdzlCZHVLUXZhT3pNYmFyeGhkVFRqWmp1cFR0RkFZOTl0WmhVPQ==";
        line29.GTIN = "04601907002805";
        line29.IndustryAttributeIdentifierFoiv = "030";
        line29.IndustryAttributeDocumentDate = "21.11.2023";
        line29.IndustryAttributeDocumentNumber = "1944";
        line29.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        var line30 = connector.AddPosition("ТНМ", 1, 10, 10.0m, TaxRate.Vat10, SignCalculationObject.MarkedWithoutCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line30.TaxSum = 0;
        line30.SignSubjectCalculationAgent = AgentType.Agent;

        var line31 = connector.AddPosition("ТМ", 1, 10, 10.0m, TaxRate.Vat10, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        line31.TaxSum = 0.91m;
        line31.SignSubjectCalculationAgent = AgentType.Agent;
        line31.Cis_Base64 = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        line31.FractionalQuantityNumerator = 1;
        line31.FractionalQuantityDenominator = 4;
        line31.IndustryAttributeIdentifierFoiv = "030";
        line31.IndustryAttributeDocumentDate = "21.11.2023";
        line31.IndustryAttributeDocumentNumber = "1944";
        line31.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        var line32 = connector.AddPosition("Сыр", 0.5m, 20, 10.0m, TaxRate.Vat10, SignCalculationObject.MarkedWithCode, SignMethodCalculation.FullPayment, MeasureOfQuantity.Kilogram);
        line32.TaxSum = 0.91m;
        line32.SignSubjectCalculationAgent = AgentType.Agent;
        line32.Cis_Base64 = "MDEwNDYwMjIyMDAwNjU0OTIxNW9wRmNtSx05M2RHVno=";
        line32.IndustryAttributeIdentifierFoiv = "030";
        line32.IndustryAttributeDocumentDate = "21.11.2023";
        line32.IndustryAttributeDocumentNumber = "1944";
        line32.IndustryAttributeAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";

        connector.AddBarcode(BarcodeType.QR, "https://shop.ru", PrintAlignment.Center);
        connector.AddPicture(Convert.ToBase64String(File.ReadAllBytes(filePath)), PictureAlignment.Right);
        connector.AddSeparatorLine(LineStyle.Dotted);

        connector.Cash = 50;
        connector.ElectronicPayment = 70;
        connector.AdvancePayment = 70;
        connector.Credit = 70;
        connector.CashProvision = 60;

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
