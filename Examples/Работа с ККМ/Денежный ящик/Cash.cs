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
        if (await connector.GetCash())
        {
            Console.WriteLine($"Остаток наличных в денежном ящике: {connector.CashBalance}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}