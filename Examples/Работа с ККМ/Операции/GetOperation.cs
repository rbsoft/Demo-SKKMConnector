using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperation : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "Операция по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetGetOperation()
    {
        await connector.GetOperation(documentId);
        if (connector.Ok)
            Console.WriteLine(
                "Операция получена.\n" +
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
