using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckCopy : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Копия чека по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 2;

    public async Task<SkkmConnector> PostPrintCheckCopy()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // DocId ранее пробитого чека (PrintCheck*, GetCheckList или панель) — по нему печатают копию на ленте:
        connector.DocId = documentId;
        if (await connector.PrintCheckCopy())
        {
            Console.WriteLine("Копия распечатана!");




        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}