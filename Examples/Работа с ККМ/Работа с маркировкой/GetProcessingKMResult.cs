using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetProcessingKMResult : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Результат ОИСМ";
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetGetProcessingKMResult()
    {
        connector.DeviceName = deviceName;
        await connector.GetProcessingKMResult();

        if (connector.Ok)
            Console.WriteLine(
                "Результат ОИСМ получен.\n" +
                $"Идентификатор запроса (Guid): {connector.MarkingProcessingGuid}\n" +
                $"Итог проверки: {connector.MarkingProcessingResult}\n" +
                $"Код результата (тег 2106): {connector.MarkingProcessingResultCode}\n" +
                $"Статус информации о КМ (тег 2109): {connector.MarkingProcessingStatusInfo}\n" +
                $"Код обработки (тег 2105): {connector.MarkingProcessingHandleCode}\n" +
                $"Статус получения от ОИСМ: {connector.MarkingProcessingRequestStatus} (0 — получен, 1 — ещё нет, 2 — не может быть получен)");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}