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

        // Получаем счётчики смены:
        if (await connector.GetTotals())
        {
            Console.WriteLine($"Номер смены: {connector.ShiftNumber}");
            Console.WriteLine($"Сумма наличных в денежном ящике: {connector.CashBalance}");
            Console.WriteLine($"Сумма внесений: {connector.ShiftIncomeSum}");
            Console.WriteLine($"Кол-во внесений: {connector.ShiftIncomeCount}");
            Console.WriteLine($"Сумма выемок: {connector.ShiftOutcomeSum}");
            Console.WriteLine($"Кол-во выемок: {connector.ShiftOutcomeCount}");
            foreach (var row in connector.Counter)
            {
                Console.WriteLine($"Тип операции: {row.Operation}");
                Console.WriteLine($"Количество операций: {row.Count}");
                Console.WriteLine($"Сумма операций: {row.Sum}");
                Console.WriteLine($"Кол-во коррекций: {row.SalesCorrectionCount}");
                Console.WriteLine($"Сумма коррекций: {row.SalesCorrectionSum}");
                Console.WriteLine($"Сумма оплат наличными: {row.Cash}");
                Console.WriteLine($"Сумма оплат безналичными: {row.Electronically}");
                Console.WriteLine($"Сумма оплат авансом: {row.Prepaid}");
                Console.WriteLine($"Сумма оплат в кредит: {row.Credit}");
                Console.WriteLine($"Сумма оплат встречным предоставлением / бартером: {row.Barter}");
                Console.WriteLine($"Сумма предоставленных скидок: {row.DiscountSum}");
                Console.WriteLine($"Кол-во предоставленных скидок: {row.DiscountCount}");
                Console.WriteLine($"Сумма надбавок: {row.AddingSum}");
                Console.WriteLine($"Кол-во надбавок: {row.AddingCount}");
            }

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");


        return connector;
    }
}