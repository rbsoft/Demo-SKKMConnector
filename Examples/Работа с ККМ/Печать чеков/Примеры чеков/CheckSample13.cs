using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Агент и поставщик указаны в реквизитах позиции (не в заголовке чека).
/// </summary>
public class CheckSample13 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Агент в позиции";
    public const int SortOrder = 23;

    public async Task<SkkmConnector> PostCheckSample13()
    {
        // Имя ККТ:
        connector.DeviceName = deviceName;
        // ФИО кассира:
        connector.CashierName = cashierName;
        // ИНН кассира (необязательно):
        connector.CashierVatin = cashierVatin;
        // NewCheck — очищает предыдущий чек и задаёт тип операции, систему налогообложения
        // CheckType.Sale задаёт чеку тип операции «приход».
        // TaxSystem.Osn задаёт чеку систему налогообложения ОСН (общая).
        connector.NewCheck(CheckType.Sale, TaxSystem.Osn);
        // Добавляем позицию в чек (агент и поставщик задаются ниже по строке, не в заголовке):
        var line1 = connector.AddPosition("Вода 1л.",
            1m,
            65m,
            65m,
            TaxRate.None, // без НДС
            SignCalculationObject.Goods, // предмет расчёта — товар
            SignMethodCalculation.FullPayment, // полный расчёт
            MeasureOfQuantity.Piece); // штуки
        // Агент в позиции (реквизиты агента по строке, не в заголовке чека):
        line1.SignSubjectCalculationAgent = AgentType.Agent; // признак агента по строке
        line1.PayingAgentOperation = "Приём платежей"; // наименование операции агента
        line1.PayingAgentPhone = new[] { "+79001234567" }; // телефон платёжного агента
        line1.ReceivePaymentsOperatorPhone = new[] { "+79001234567" }; // телефон оператора по приёму платежей
        // Поставщик в позиции
        line1.VendorDataName = "ООО Ромашка"; // наименование поставщика
        line1.VendorDataPhones = new[] { "+79001234567" }; // телефон поставщика
        line1.VendorDataVatin = "7701234560"; // ИНН поставщика
        connector.Cash = 65m; // сумма наличными
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
