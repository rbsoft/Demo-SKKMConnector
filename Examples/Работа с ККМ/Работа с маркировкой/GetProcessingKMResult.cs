using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetProcessingKMResult : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Результат ОИСМ";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetGetProcessingKMResult()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Получаем результат проверки:
        if (await connector.GetProcessingKMResult())
        {
            Console.WriteLine($"Уникальный код запроса: {connector.Guid}");
            Console.WriteLine($"Итог проверки: {connector.KmResult}");
            Console.WriteLine($"Код результата проверки: {connector.ResultCode}");
            Console.WriteLine($"Статус информации о коде: {connector.StatusInfo}");
            Console.WriteLine($"Код обработки запроса: {connector.HandleCode}");
            Console.WriteLine($"Статус запроса: {connector.RequestStatus}");
            RequestKM.PrintCodeMark(connector.CodeMarkInfo);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");



        return connector;
    }
}