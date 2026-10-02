using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class DeleteCheckTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Шаблоны чека";
    public const string Title = "Удаление шаблона чека";

    public async Task<SkkmConnector> DeleteDeleteCheckTemplate()
    {
        await connector.DeleteCheckTemplate("sale_template_01");
        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
