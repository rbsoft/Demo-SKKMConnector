using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationLast : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "Последняя операция";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> GetGetOperationLast()
    {
        connector.PaymentType = CheckType.Sale;
        connector.IsProcessed = true;
        await connector.GetOperationLast();
        if (connector.Ok)
            Console.WriteLine(
                "Последняя операция.\n" +
                $"docId: {connector.OperationDocId}\n" +
                $"Тип задания: {connector.OperationTaskType}\n" +
                $"Дата: {connector.OperationDate:dd.MM.yyyy HH:mm}\n" +
                $"Имя кассы: {connector.OperationDeviceName}\n" +
                $"Обработан: {connector.OperationIsProcessed}\n" +
                $"Код результата: {connector.OperationResultCode}\n" +
                $"Описание результата: {connector.OperationResultDescription}\n" +
                $"Базовый документ (docId): {connector.OperationBaseDocId}\n" +
                $"Идентификатор запроса: {connector.OperationRequestId}\n" +
                $"Терминал: {connector.OperationTerminalId}, пул: {connector.OperationPoolId}\n" +
                $"Версия сервера: {connector.OperationServerVersion}, версия клиента: {connector.OperationClientVersion}\n" +
                $"Отправитель: {connector.OperationSenderAppName} {connector.OperationSenderAppVersion}\n" +
                $"Устройство: {connector.OperationDeviceModel} (сер. {connector.OperationDeviceSerialNumber}, прошивка {connector.OperationDeviceFirmwareVersion})");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
