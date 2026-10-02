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
        await connector.GetFiscalSign(1);
        if (connector.Ok)
            Console.WriteLine($"ФП: {connector.FiscalSign}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
