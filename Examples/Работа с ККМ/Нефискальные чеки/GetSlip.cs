using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetSlip : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Получение нефискального документа";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetSlip()
    {
        connector.DeviceName = deviceName;
        await connector.GetSlip(documentId);
        if (connector.Ok)
        {
            Console.WriteLine(
                "Слип получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Дата создания: {connector.CheckDate:dd.MM.yyyy HH:mm}\n" +
                $"Тип документа: {connector.CheckTaskType}\n" +
                $"Номер ФД: {connector.CheckNumber}, ФП: {connector.FiscalSign}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Имя кассы: {connector.CheckDeviceName}, пул: {connector.CheckPoolId}, терминал: {connector.CheckTerminalId}\n" +
                $"Обработан успешно: {connector.CheckIsProcessed}, код: {connector.CheckResultCode} {connector.CheckResultDescription}\n" +
                $"Версия сервера ККМ: {connector.CheckServerVersion}\n" +
                $"Строк печатной формы: {connector.PrintForm.Count}");
            foreach (var line in connector.PrintForm)
            {
                var text = string.IsNullOrEmpty(line.LineRight) ? line.Line : $"{line.Line} … {line.LineRight}";
                Console.WriteLine($"  [{line.Type}] {text}");
            }
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
