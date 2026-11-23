using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class PrintCheckAsync : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Асинхронная печать чека";
    public const int SortOrder = 1;

    public async Task<SkkmConnector> PostPrintCheckAsync()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale задаёт чеку тип операции «приход».
        // TaxSystem.Envd задаёт чеку систему налогообложения ЕНВД.
        connector.NewCheck(CheckType.Sale, TaxSystem.Envd);
        connector.IsElectronically = false;

        // Данные покупателя — для электронного чека и передачи в ОФД:
        connector.CustomerInfo = "ООО 'Рога и Копыта'";
        connector.CustomerVatin = "500100732259"; // ИНН покупателя
        connector.CustomerEmail = "kuznicov@mail.ru"; // e-mail для отправки чека

        // Первая позиция — услуга с НДС 0%:
        var line1 = connector.AddPosition("Бутылка с водой 1л.", 1, 30, 30, TaxRate.Vat0, // ставка НДС 0%
            SignCalculationObject.Service, // предмет расчёта — услуга
            SignMethodCalculation.FullPayment); // способ расчёта — полный расчёт
        line1.DiscountSum = 0; // скидка уменьшает сумму строки; 0 — без скидки
        line1.Department = 1; // отдел (секция) на ККТ
        line1.TaxSum = 0; // сумма НДС по строке
        // Вторая позиция — товар с НДС 20%, мера — штуки:
        var line2 = connector.AddPosition("Бутылка с крышкой (пустая) 4л.", 1, 30, 30, TaxRate.Vat20, // ставка НДС 20%
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // способ расчёта — полный расчёт
            MeasureOfQuantity.Piece); // мера — штуки
        line2.DiscountSum = 0; // скидка уменьшает сумму строки; 0 — без скидки
        line2.Department = 1; // отдел (секция) на ККТ
        line2.TaxSum = 5; // сумма НДС по строке
        connector.Cash = 30; // сумма наличными
        if (await connector.PrintCheckAsync())
        {
                        // DocId задания в очереди — им по нему смотрят GetTaskStatus / GetQueueTask:
            Console.WriteLine($"Идентификатор документа: {connector.DocId}");

        }
        else
            Console.WriteLine($"Код ошибки: {connector.ErrorCode} {connector.ErrorDescription}");

        return connector;
    }
}
