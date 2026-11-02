using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintSlipAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Асинхронная печать нефискального документа";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostPrintSlipAsync()
    {
        connector.DeviceName = deviceName;
        connector.Text =
            "[center,bold]РБ-Софт: Сервер ККМ\n" +
            "[center]Нефискальный документ\n" +
            "[line]\n" +
            "Обычная строка текста\n" +
            "[QR,center]https://www.rbsoft.ru\n" +
            "[center,small]Спасибо за покупку!";
        if (await connector.PrintSlipAsync())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}