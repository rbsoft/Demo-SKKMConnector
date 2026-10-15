using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckList : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Список чеков за смену";
    public const int SortOrder = 5;

    public async Task<SkkmConnector> GetGetCheckList()
    {
        connector.DeviceName = deviceName;
        connector.FromTo(fromDate, toDate);

        await connector.GetCheckList(54);

        if (connector.Ok)
        {
            Console.WriteLine($"Найдено чеков: {connector.Checks.Count}");
            foreach (var check in connector.Checks)
            {
                Console.WriteLine(
                    $"ФД={check.DocNumber}, ФП={check.FiscalSign}, смена={check.ShiftNumber}, " +
                    $"№ за смену={check.DocNumberInShift}, сумма={check.Sum}, дата={check.FiscalDate:dd.MM.yyyy HH:mm}, " +
                    $"СНО={check.TaxType}, кассир={check.CashierName}, docId={check.DocId}\n" +
                    $"  Организация: {check.OrganizationInfo}, ИНН: {check.OrganizationVatin}, ОФД: {check.OfdName}\n" +
                    $"  QR: сумма={check.QrDataAmount}, ФН={check.QrDataFn}, ФД={check.QrDataFd}, ФП={check.QrDataFp}\n" +
                    $"  Оплата: наличными={check.PaymentsCash}, безнал={check.PaymentsElectronic}, позиций={check.ItemsCount}");

                foreach (var item in check.Items)
                {
                    Console.WriteLine($"    {item.Name}: {item.Quantity} × {item.Price} = {item.Sum}, НДС={item.TaxValue}, сумма НДС={item.TaxSum}");
                    if (!string.IsNullOrEmpty(item.MarkingCode) || item.ImcCheckFlag)
                        Console.WriteLine($"      КМ={item.MarkingCode}, проверка={item.ImcCheckResult}, отрасл.реквизит={item.IndustryAttributeValue}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");
        }

        return connector;
    }
}
