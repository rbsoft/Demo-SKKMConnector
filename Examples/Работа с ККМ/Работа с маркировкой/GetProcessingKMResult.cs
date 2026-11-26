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
        if (await connector.GetProcessingKMResult())
        {
            Console.WriteLine($"Уникальный идентификатор запроса: {connector.Guid}");
            Console.WriteLine($"Итог проверки кода маркировки в ОИСМ: {connector.KmResult}");
            Console.WriteLine($"Код результата: {connector.ResultCode}");
            Console.WriteLine($"Статус информации о коде маркировки (тег 2109): {connector.StatusInfo}");
            Console.WriteLine($"Код обработки запроса (тег 2105): {connector.HandleCode}");
            Console.WriteLine($"Статус получения результата от ОИСМ: {connector.RequestStatus}");
            if (connector.CodeMarkInfo != null)
            {
                Console.WriteLine($"КИ/КиЗ: {connector.CodeMarkInfo.Cis}");
                Console.WriteLine($"Структура КИ/КиЗ валидна: {connector.CodeMarkInfo.Valid}");
                Console.WriteLine($"Криптопроверка пройдена: {connector.CodeMarkInfo.Verified}");
                Console.WriteLine($"КМ найден: {connector.CodeMarkInfo.Found}");
                Console.WriteLine($"GTIN: {connector.CodeMarkInfo.Gtin}");
                Console.WriteLine($"Товар продан: {connector.CodeMarkInfo.Sold}");
                Console.WriteLine($"КМ заблокирован: {connector.CodeMarkInfo.IsBlocked}");
                Console.WriteLine($"Текст ошибки: {connector.CodeMarkInfo.Message}");
            }

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
