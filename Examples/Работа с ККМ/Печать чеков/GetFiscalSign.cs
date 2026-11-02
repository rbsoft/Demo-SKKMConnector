using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetFiscalSign : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Фискальный признак";
    public const int SortOrder = 6;

    public async Task<SkkmConnector> GetFiscalSignByDocNumber()
    {
        connector.DeviceName = deviceName;
        // Номер фискального документа (ФД):
        connector.CheckNumber = checkNumber;

        if (await connector.GetFiscalSign())
        {
            Console.WriteLine($"Фискальный признак документа: {connector.FiscalSign}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
