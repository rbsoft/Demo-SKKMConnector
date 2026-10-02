using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Cash : Sample
{
    public const string GroupPath = "Работа с ККМ|Денежный ящик";
    public const string Title = "Остаток в ящике";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> GetCash()
    {
        connector.DeviceName = deviceName;
        await connector.GetCash();

        if (connector.Ok)
            Console.WriteLine($"Остаток наличных: {connector.CashBalance}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}