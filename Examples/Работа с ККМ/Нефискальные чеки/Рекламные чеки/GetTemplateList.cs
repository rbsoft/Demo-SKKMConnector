using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetTemplateList : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Рекламные чеки";
    public const string Title = "Список шаблонов печати";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetTemplateList()
    {
        await connector.GetTemplateList();
        if (connector.Ok)
        {
            Console.WriteLine($"Шаблонов печати: {connector.Templates.Length}");
            foreach (var name in connector.Templates)
                Console.WriteLine($"  {name}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
