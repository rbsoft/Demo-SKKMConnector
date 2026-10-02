using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintSlip : Sample
{
    public const string GroupPath = "Работа с ККМ|Нефискальные чеки";
    public const string Title = "Печать нефискального документа";
    public const int SortOrder = 0;

    public async Task<SkkmConnector> PostPrintSlip()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.TextForPrint =
            "[center,bold]РБ-Софт: Сервер ККМ\n" +
            "[center]Нефискальный документ\n" +
            "[line]\n" +
            "Обычная строка текста\n" +
            "[QR,center]https://www.rbsoft.ru\n" +
            "[center,small]Спасибо за покупку!";
        await connector.PrintSlip();

        if (connector.Ok)
        {
            Console.WriteLine(
                "Слип напечатан.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Дата создания: {connector.CheckDate:dd.MM.yyyy HH:mm}\n" +
                $"Тип документа: {connector.CheckTaskType}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Имя кассы: {connector.CheckDeviceName}, пул: {connector.CheckPoolId}, терминал: {connector.CheckTerminalId}\n" +
                $"Обработан успешно: {connector.CheckIsProcessed}, код: {connector.CheckResultCode} {connector.CheckResultDescription}\n" +
                $"Версия сервера ККМ: {connector.CheckServerVersion}\n" +
                $"Напечатано строк: {connector.PrintForm.Count}");
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