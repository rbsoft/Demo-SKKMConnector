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

        // Уникальный код запроса (Guid) коннектор генерирует сам и возвращает в connector.Guid —
        // он понадобится для ConfirmKM. Заданный в поле Guid не используется.
        connector.NotSendToServer = notSendToServer;     // Результат проверки не отправлять в ОИСМ
        connector.WaitForResult = waitForResult;         // Ждать ответа ОИСМ
        connector.MarkingCode = markingCode;             // КМ в Base64
        connector.PlannedStatus = plannedStatus;         // Планируемый статус (таблица 105 ФФД)
        connector.Quantity = markingQuantity;            // Количество товара
        connector.MeasureOfQuantity = measureOfQuantity; // Мера количества (таблица 114 ФФД)
        connector.FractionalQuantityNumerator = fractionalQuantityNumerator;     // Числитель дробного количества
        connector.FractionalQuantityDenominator = fractionalQuantityDenominator; // Знаменатель дробного количества

        // Проверяем КМ:
        if (await connector.RequestKM())
        {
            Console.WriteLine($"Связь с ОИСМ: {connector.IsmConnected}");
            Console.WriteLine($"Проверка формата кода: {connector.FormatChecking}");
            Console.WriteLine($"Проверка кода маркировки: {connector.Checking}");
            Console.WriteLine($"Результат проверки: {connector.CheckingResult}");
            Console.WriteLine($"Штрихкод: {connector.MarkingBarcode}");
            PrintCodeMark(connector.CodeMarkInfo);
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }

    internal static void PrintVerify(SkkmConnector connector)
    {
        Console.WriteLine($"Код результата: {connector.MarkingAnswerCode}");
        Console.WriteLine($"Описание: {connector.MarkingDescription}");
        Console.WriteLine($"Идентификатор запроса: {connector.ReqId}");
        Console.WriteLine($"Время проверки: {connector.ReqTimestamp}");
        Console.WriteLine($"Статус модуля: {connector.MarkingModuleStatus}");
        Console.WriteLine($"Нужно обновить базу: {connector.RequiresDownload}");
        Console.WriteLine($"Обновление базы: {connector.MarkingLastUpdate}");
        Console.WriteLine($"Синхронизация: {connector.MarkingLastSync}");
        Console.WriteLine($"Офлайн-проверка: {connector.IsCheckedOffline}");
        Console.WriteLine($"Версия модуля: {connector.MarkingModuleVersion}");
        Console.WriteLine($"Экземпляр модуля: {connector.MarkingModuleId}");
        Console.WriteLine($"Адрес модуля: {connector.MarkingServiceUrl}");
        foreach (var mark in connector.MarkingCodes)
            PrintCodeMark(mark);
    }

    internal static void PrintCodeMark(CodeMarkInfo? mark)
    {
        if (mark == null)
            return;
        Console.WriteLine($"КИ: {mark.Cis}");
        Console.WriteLine($"DataMatrix: {mark.DataMatrix}");
        Console.WriteLine($"Структура валидна: {mark.Valid}");
        Console.WriteLine($"КИ без криптоподписи: {mark.PrintView}");
        Console.WriteLine($"Товарные группы: {string.Join(", ", mark.GroupIds)}");
        Console.WriteLine($"Криптопроверка: {mark.Verified}");
        Console.WriteLine($"В обороте: {mark.Realizable}");
        Console.WriteLine($"Нанесён: {mark.Utilised}");
        Console.WriteLine($"Найден: {mark.Found}");
        Console.WriteLine($"Срок годности: {mark.ExpireDate}");
        Console.WriteLine($"Вариативные сроки: {mark.VariableExpirations}");
        Console.WriteLine($"Дата производства: {mark.ProductionDate}");
        Console.WriteLine($"Вес: {mark.ProductWeight}");
        Console.WriteLine($"Ветеринарный документ: {mark.PrVetDocument}");
        Console.WriteLine($"Принадлежит ИНН: {mark.IsOwner}");
        Console.WriteLine($"Код ошибки КМ: {mark.ErrorCode}");
        Console.WriteLine($"Сервисный код: {mark.ServiceCode}");
        Console.WriteLine($"Сообщение: {mark.Message}");
        Console.WriteLine($"Прослеживаемость: {mark.IsTracking}");
        Console.WriteLine($"Продан: {mark.Sold}");
        Console.WriteLine($"GTIN: {mark.Gtin}");
        Console.WriteLine($"Тип упаковки: {mark.PackageType}");
        Console.WriteLine($"Агрегат: {mark.Parent}");
        Console.WriteLine($"ИНН производителя: {mark.ProducerInn}");
        Console.WriteLine($"МРЦ: {mark.Mrp}");
        Console.WriteLine($"Серая зона: {mark.GrayZone}");
        Console.WriteLine($"ЕМЦ: {mark.Smp}");
        Console.WriteLine($"Заблокирован: {mark.IsBlocked}");
        Console.WriteLine($"Некорректный GTIN: {mark.IsGreyGtin}");
        Console.WriteLine($"Органы блокировки: {string.Join(", ", mark.Ogvs)}");
        Console.WriteLine($"Вложение: {mark.InnerUnitCount}");
        Console.WriteLine($"Продано или возвращено: {mark.SoldUnitCount}");
        Console.WriteLine($"Серия: {mark.ProductionSerialNumber}");
        Console.WriteLine($"Партия: {mark.ProductionBatchNumber}");
        Console.WriteLine($"Заводской номер: {mark.FactorySerialNumber}");
        Console.WriteLine($"Ёмкость упаковки: {mark.PackageQuantity}");
        Console.WriteLine($"Выбытие: {mark.EliminationState}");
    }
}