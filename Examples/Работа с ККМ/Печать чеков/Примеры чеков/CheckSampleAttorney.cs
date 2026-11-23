using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Поверенный в заголовке (AgentSign = 4) — как в демо 1С.
/// </summary>
public class CheckSampleAttorney : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Поверенный";
    public const int SortOrder = 20;

    public async Task<SkkmConnector> PostCheckSampleAttorney()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        // 4 — поверенный; признак на весь чек (только данные поставщика)
        connector.AgentSign = AgentType.Attorney;
        connector.VendorDataName = "ООО Ромашка";
        connector.VendorDataPhones = new[] { "+79001234567" };
        connector.VendorDataVatin = "7701234560";

        connector.AddPosition("Вода 1л.", 1m, 62m, 62m, TaxRate.None, SignCalculationObject.Goods, SignMethodCalculation.FullPayment, MeasureOfQuantity.Piece);
        connector.Cash = 62m;

        if (await connector.PrintCheck())
        {
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");
            Console.WriteLine($"Время операции: {connector.ServerTime}");
            Console.WriteLine($"Адрес сайта уполномоченного органа(ФНС) в сети «Интернет»: {connector.FnFnsUrl}");
            Console.WriteLine($"Серийный номер фискального накопителя: {connector.FnSerialNumber}");
            Console.WriteLine($"Регистрационный номер ККТ: {connector.RnNumber}");
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Номер чека: {connector.CheckNumber}");
            Console.WriteLine($"Дата и время документа по часам ФН: {connector.FiscalDatetime}");
            Console.WriteLine($"ФП чека: {connector.FiscalSign}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
