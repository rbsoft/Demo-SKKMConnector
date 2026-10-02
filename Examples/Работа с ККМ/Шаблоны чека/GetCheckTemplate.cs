using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Шаблоны чека";
    public const string Title = "Получение шаблона чека";

    public async Task<SkkmConnector> GetGetCheckTemplate()
    {
        await connector.GetCheckTemplate("piot_test_classic_5.01");

        if (connector.Ok)
        {
            Console.WriteLine(
                $"Шаблон: {connector.CheckTemplateName}\n" +
                $"Тип чека: {connector.CheckTemplatePaymentType}, СНО: {connector.CheckTemplateTaxVariant}, часовая зона: {connector.CheckTemplateTimeZone}\n" +
                $"Сумма: {connector.CheckTemplateSum}, сдача: {connector.CheckTemplateChange}\n" +
                $"Фискальный: {connector.CheckTemplateIsFiscal}, без печати: {connector.CheckTemplateElectronically}, расчёт в интернете: {connector.CheckTemplateOperationOnline}\n" +
                $"Замена ставки НДС: {connector.CheckTemplateIsReplaceTax}, подтверждён в ФН: {connector.CheckTemplateTrustedInFn}\n" +
                $"Номер МТ: {connector.CheckTemplateMtNumber}, ошибка печати: {connector.CheckTemplatePrintError}\n" +
                $"Доп. реквизит чека: {connector.CheckTemplateAdditionalAttribute}\n" +
                $"Оплата: наличными={connector.CheckTemplatePaymentsCash}, безнал={connector.CheckTemplatePaymentsElectronic}, " +
                $"аванс={connector.CheckTemplatePaymentsPrePaid}, кредит={connector.CheckTemplatePaymentsCredit}, встречное={connector.CheckTemplatePaymentsBarter}\n" +
                $"Покупатель: {connector.CheckTemplateCustomerInfo}, ИНН: {connector.CheckTemplateCustomerInn}, " +
                $"почта: {connector.CheckTemplateCustomerEmail}, телефон: {connector.CheckTemplateCustomerPhone}\n" +
                $"Коррекция: {connector.CheckTemplateCorrectionType}, {connector.CheckTemplateCorrectionDescription}\n" +
                $"Позиций: {connector.CheckTemplateItems.Count}");
            foreach (var item in connector.CheckTemplateItems)
                Console.WriteLine(
                    $"  {item.Name}: {item.Quantity} × {item.Price} = {item.Sum}, скидка={item.DiscountInfoValue}\n" +
                    $"    НДС={item.TaxValue}, сумма НДС={item.TaxSum}, отдел={item.Department}, " +
                    $"способ расчёта={item.PaymentMode}, предмет расчёта={item.ItemType}, мера={item.MeasureOfQuantity}\n" +
                    $"    акциз={item.ExciseAmount}, КМ={item.MarkingCode}, код товара={item.ProductCode}, доп. реквизит={item.AdditionalAttribute}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
