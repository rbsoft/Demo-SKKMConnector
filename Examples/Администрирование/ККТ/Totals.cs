using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Totals : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Итоги смены";
    public const int SortOrder = 9;

    public async Task<SkkmConnector> GetTotals()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        if (await connector.GetTotals())
        {
            Console.WriteLine($"Имя устройства: {connector.ShiftNumber}");
            Console.WriteLine($"Сумма наличных в денежном ящике: {connector.CashBalance}");
            Console.WriteLine($"Сумма внесений: {connector.ShiftIncomeSum}");
            Console.WriteLine($"Кол-во внесений: {connector.ShiftIncomeCount}");
            Console.WriteLine($"Сумма выемок: {connector.ShiftOutcomeSum}");
            Console.WriteLine($"Кол-во выемок: {connector.ShiftOutcomeCount}");
            foreach (var counter in connector.Counter)
                Console.WriteLine($"Операция: {counter.Operation} кол-во: {counter.Count} сумма: {counter.Sum} корр. кол-во: {counter.SalesCorrectionCount} корр. сумма: {counter.SalesCorrectionSum} наличные: {counter.Cash} безнал: {counter.Electronically} аванс: {counter.Prepaid} кредит: {counter.Credit} бартер: {counter.Barter} скидка сумма: {counter.DiscountSum} скидка кол-во: {counter.DiscountCount} надбавка сумма: {counter.AddingSum} надбавка кол-во: {counter.AddingCount}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
