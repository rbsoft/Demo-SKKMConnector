using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class DeleteTemplate : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки|Рекламные чеки";
    public const string Title = "Удаление шаблона печати";
    public const int SortOrder = 2;

    public async Task<SkkmConnector> DeleteDeleteTemplate()
    {
        connector.TemplateName = "name1221212121220";
        await connector.DeleteTemplate();
        if (connector.Ok)
            Console.WriteLine("Выполнено успешно.");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
