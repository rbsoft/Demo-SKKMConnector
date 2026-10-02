using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckTemplateList : Sample
{
    public const string GroupPath = "Работа с ККМ|Шаблоны чека";
    public const string Title = "Список шаблонов чека";

    public async Task<SkkmConnector> GetGetCheckTemplateList()
    {
        await connector.GetCheckTemplateList();
        if (connector.Ok)
        {
            Console.WriteLine($"Шаблонов чека: {connector.CheckTemplates.Length}");
            foreach (var t in connector.CheckTemplates)
                Console.WriteLine($"  {t.Name} (TaskType {t.TaskType})");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
