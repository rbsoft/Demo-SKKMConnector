using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class RequestKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ";
    public const bool NeedGuid = true;
    public const bool GuidOptional = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostRequestKM()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;

        // Уникальный код запроса (Guid) коннектор генерирует сам
        // он понадобится для ConfirmKM. Заданный в поле Guid не используется.
        connector.NotSendToServer = notSendToServer;
        // Ждать ответ ОИСМ в этом же вызове:
        connector.WaitForResult = waitForResult;
        // КМ в Base64 (как в DataMatrix) для RequestKM:
        connector.MarkingCode = markingCode;
        // Планируемый статус товара (таблица 105 ФФД) при продаже/возврате:
        connector.PlannedStatus = plannedStatus;
        // Количество маркированного товара:
        connector.Quantity = markingQuantity;
        // Единица измерения количества:
        connector.MeasureOfQuantity = measureOfQuantity;
        // Числитель дробного количества (весовой товар):
        connector.FractionalQuantityNumerator = fractionalQuantityNumerator;
        // Знаменатель дробного количества:
        connector.FractionalQuantityDenominator = fractionalQuantityDenominator;
        if (await connector.RequestKM())
        {
            Console.WriteLine($"Связь с ОИСМ: {connector.IsmConnected}");
            Console.WriteLine($"Проверка формата кода: {connector.FormatChecking}");
            Console.WriteLine($"Проверка кода маркировки: {connector.Checking}");
            Console.WriteLine($"Результат проверки: {connector.CheckingResult}");
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
