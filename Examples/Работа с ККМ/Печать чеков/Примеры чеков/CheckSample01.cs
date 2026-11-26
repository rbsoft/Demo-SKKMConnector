using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Приход — базовый чек 
/// </summary>
public class CheckSample01 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Приход";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostCheckSample01()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck(1, 0) — приход, ОСН
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);

        connector.AddPosition(
            "Вода 1л.",
            1m,
            50m,
            50m,
            TaxRate.None,
            SignCalculationObject.Goods,
            SignMethodCalculation.FullPayment,
            MeasureOfQuantity.Piece);

        connector.Cash = 50m;

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
