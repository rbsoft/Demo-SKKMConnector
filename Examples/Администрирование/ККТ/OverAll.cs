using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class OverAll : Sample
{
    public const string GroupPath = "Администрирование|ККТ";
    public const string Title = "Общие счётчики";
    public const int SortOrder = 13;

    public async Task<SkkmConnector> GetOverall()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        if (await connector.GetOverall())
        {
            Console.WriteLine($"Не обнуляемая сумма: {connector.NonZeroSum}");
            Console.WriteLine($"Сумма наличных в денежном ящике: {connector.CashAmount}");
            foreach (var counter in connector.Counter)
                Console.WriteLine($"Операция: {counter.Operation} кол-во: {counter.Count} сумма: {counter.Sum} корр. кол-во: {counter.SalesCorrectionCount} корр. сумма: {counter.SalesCorrectionSum} наличные: {counter.Cash} безнал: {counter.Electronically} аванс: {counter.Prepaid} кредит: {counter.Credit} бартер: {counter.Barter} скидка сумма: {counter.DiscountSum} скидка кол-во: {counter.DiscountCount} надбавка сумма: {counter.AddingSum} надбавка кол-во: {counter.AddingCount}");
        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}