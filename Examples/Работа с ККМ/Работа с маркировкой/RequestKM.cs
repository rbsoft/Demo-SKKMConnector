using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class RequestKM : Sample
{
    public const string GroupPath = "Работа с ККМ|Работа с маркировкой";
    public const string Title = "Проверка КМ";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> PostRequestKM()
    {
        connector.DeviceName = deviceName;
        connector.MarkingCode = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        connector.PlannedStatus = MarkingPlannedStatus.Sold;
        connector.MarkingQuantity = 1;
        connector.WaitForResult = true;
        await connector.RequestKM();

        if (connector.Ok)
            Console.WriteLine(
                "Проверка КМ выполнена.\n" +
                $"ИСМ на связи: {connector.MarkingCheckIsmConnected}\n" +
                $"Проверка формата КМ: {connector.MarkingCheckFormatChecking}\n" +
                $"Проверка КМ в ОИСМ: {connector.MarkingCheckChecking}\n" +
                $"Результат проверки: {connector.MarkingCheckCheckingResult}\n" +
                $"Штрихкод: {connector.MarkingCheckBarcode}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}