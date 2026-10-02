using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class UpdateCheckTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Шаблоны чека";
    public const string Title = "Изменение шаблона чека";

    public async Task<SkkmConnector> PutUpdateCheckTemplate()
    {
        connector.NewCheckTemplate("sale_template_01", CheckType.Sale);
        connector.AddPosition("Товар1", 1, 12, TaxRate.Vat0,
            SignCalculationObject.ТМ, SignMethodCalculation.FullPayment);
        connector.Cash = 12;

        await connector.UpdateCheckTemplate();
        if (connector.Ok)
            Console.WriteLine($"Шаблон чека обновлён: {connector.TemplateName}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
