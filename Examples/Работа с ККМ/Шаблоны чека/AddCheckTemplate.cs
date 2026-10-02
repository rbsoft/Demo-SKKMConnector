using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class AddCheckTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Шаблоны чека";
    public const string Title = "Создание шаблона чека";

    public async Task<SkkmConnector> PostAddCheckTemplate()
    {
        connector.NewCheckTemplate("sale_template_01", CheckType.Sale);
        connector.AddPosition("Товар", 1, 101, TaxRate.Vat0,
            SignCalculationObject.ТМ, SignMethodCalculation.FullPayment);
        connector.Cash = 101;

        await connector.AddCheckTemplate();
        if (connector.Ok)
            Console.WriteLine($"Шаблон чека сохранён: {connector.TemplateName}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
