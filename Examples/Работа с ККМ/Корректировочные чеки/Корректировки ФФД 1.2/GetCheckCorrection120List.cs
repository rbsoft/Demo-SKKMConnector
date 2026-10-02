using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetCheckCorrection120List : Sample
{
    public const string GroupPath = "Работа с ККМ|Корректировочные чеки|Корректировки ФФД 1.2";
    public const string Title = "Список чеков коррекции 1.2";

    public async Task<SkkmConnector> GetCheckCorrection120ListItems()
    {
        connector.DeviceName = deviceName;
        await connector.GetCorrection120List();
        if (connector.Ok)
        {
            Console.WriteLine($"Коррекций 1.2: {connector.Corrections120.Count}");
            foreach (var c in connector.Corrections120)
                Console.WriteLine(
                    $"  docId={c.DocId}, дата={c.Date:dd.MM.yyyy HH:mm}, тип={c.TaskType}\n" +
                    $"    ФД={c.DocNumber}, № за смену={c.DocNumberInShift}, смена={c.ShiftNumber}, ФП={c.FiscalSign}, ФН={c.Fn}\n" +
                    $"    дата по часам ФН={c.FiscalDate:dd.MM.yyyy HH:mm}, сумма={c.Sum}, СНО={c.TaxType}, позиций={c.ItemsCount}\n" +
                    $"    коррекция: {c.CorrectionDataType}, основание: {c.CorrectionDataDescription}, " +
                    $"дата: {c.CorrectionDataDate:dd.MM.yyyy}, предписание: {c.CorrectionDataNumber}\n" +
                    $"    оплата: наличными={c.PaymentsCash}, безнал={c.PaymentsElectronic}, аванс={c.PaymentsPrePaid}, " +
                    $"кредит={c.PaymentsCredit}, встречное={c.PaymentsBarter}\n" +
                    $"    кассир={c.CashierName}, касса={c.DeviceName}, обработан={c.IsProcessed}, код={c.ResultCode} {c.ResultDescription}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
