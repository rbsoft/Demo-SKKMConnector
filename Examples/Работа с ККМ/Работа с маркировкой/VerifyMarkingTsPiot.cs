using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class VerifyMarkingTsPiot : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ через ТС ПИоТ";
    public const int SortOrder = 7;

    public async Task<SkkmConnector> PostVerifyMarkingTsPiot()
    {
        connector.DeviceName = deviceName;
        connector.MarkingCodes.Clear();
        connector.MarkingCodes.Add("MDEwNDY3MDU0MDE3NjA5OTIxNSdXOVVtHTkzZEdWeg==");
        await connector.VerifyMarkingTsPiot();
        if (connector.Ok)
        {
            Console.WriteLine(
                "Проверка маркировки выполнена.\n" +
                $"Код результата: {connector.MarkingResultCode}\n" +
                $"Описание: {connector.MarkingResultDescription}\n" +
                $"Проверено кодов: {connector.MarkingCodesCount}\n" +
                $"Идентификатор запроса: {connector.MarkingReqId}\n" +
                $"Метка времени запроса: {connector.MarkingReqTimestamp}\n" +
                $"Офлайн-проверка: {connector.MarkingIsCheckedOffline}\n" +
                $"Модуль: {connector.MarkingName}, версия:{connector.MarkingVersion}, статус: {connector.MarkingStatus}, режим: {connector.MarkingOperationMode}\n" +
                $"Сервис проверки: {connector.MarkingServiceUrl}\n" +
                $"ИНН модуля: {connector.MarkingInn}, версия БД: {connector.MarkingDbVersion}\n" +
                $"Требуется загрузка из ГИС МТ: {connector.MarkingRequiresDownload}\n" +
                $"Последнее обновление: {connector.MarkingLastUpdateDate} ({connector.MarkingLastUpdate}), синхронизация: {connector.MarkingLastSyncDate} ({connector.MarkingLastSync})");

            foreach (var km in connector.MarkingCodesInfo)
                Console.WriteLine(
                    $"  КМ: {km.PrintView}\n" +
                    $"    GTIN: {km.GTIN}, тип упаковки: {km.PackageType}, ИНН производителя: {km.ProducerInn}\n" +
                    $"    Валидный: {km.Valid}, крипто-проверка: {km.Verified}, в обороте: {km.Realizable}, найден в ГИС МТ: {km.Found}\n" +
                    $"    Продан: {km.Sold}, заблокирован: {km.IsBlocked}, серая зона: {km.GrayZone}, некорректный GTIN: {km.IsGreyGtin}\n" +
                    $"    Прослеживаемость: {km.IsTracking}, товарные группы: [{string.Join(",", km.GroupIds)}]\n" +
                    $"    Срок годности: {km.ExpireDate:dd.MM.yyyy}, дата производства: {km.ProductionDate:dd.MM.yyyy}, МРЦ: {km.Mrp}\n" +
                    $"    Вложений: {km.InnerUnitCount}, продано: {km.SoldUnitCount}, серийный №: {km.ProductionSerialNumber}\n" +
                    $"    Код ошибки: {km.ErrorCode}, код сервиса: {km.ServiceCode}, сообщение: {km.Message}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
