using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetFiscalizationList : Sample
{
    public const string GroupPath = "Работа с ККМ|Фискализация";
    public const string Title = "Список фискализаций";
    public const int SortOrder = 3;

    public async Task<SkkmConnector> GetGetFiscalizationList()
    {
        connector.DeviceName = deviceName;
        await connector.GetFiscalizationList();
        if (connector.Ok)
        {
            Console.WriteLine($"Фискализаций: {connector.Fiscalizations.Length}");
            foreach (var f in connector.Fiscalizations)
                Console.WriteLine(
                    $"  {f.OperationType}, docId={f.DocId}, касса={f.DeviceName}\n" +
                    $"    РНМ={f.RnNumber}, фискальный режим={f.IsFiscal}\n" +
                    $"    организация={f.CompanyName}, ИНН={f.Vatin}, СНО (коды)={f.TaxationSystems}\n" +
                    $"    ФФД ККТ={f.FfdVersionKkt}, ФФД ФН={f.FfdVersionFn}\n" +
                    $"    смена={f.ShiftNumber}, ФД={f.DocNumber}, ФП={f.FiscalSign}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
