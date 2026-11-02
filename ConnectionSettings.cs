using System.Globalization;
using RBSoftSkkm;

namespace SkkmNugetSample;

public class ConnectionSettings
{
    public const string DateFormat = "dd.MM.yyyy";
    private static readonly string[] DateFormats = [DateFormat, "yyyy-MM-dd", "d.M.yyyy"];
    public const string HintAddress = "localhost:4398";
    public const string HintToken = "api_key";
    public const string HintDevice = "Emu";
    public const string HintCashier = "Иванов А.И.";
    public const string HintCashierVatin = "7722345678";
    public const string HintFilePath = @"Путь к картинке, напр. C:\logo.png";

    public string Address { get; set; } = "";
    public string Token { get; set; } = "";
    public string Device { get; set; } = "";
    public string Cashier { get; set; } = "";
    public string CashierVatin { get; set; } = "";
    public string DocumentId { get; set; } = "";
    public string FilePath { get; set; } = "";
    public string FromText { get; set; } = "";
    public string ToText { get; set; } = "";
    public string Pool { get; set; } = "";
    public string CheckNumberText { get; set; } = "";
    public string CashAmountText { get; set; } = "";
    public string Guid { get; set; } = "";
    public KmConfirmationType ConfirmationType { get; set; } = KmConfirmationType.Included;
    public string Codes { get; set; } = "";
    public DateTime From { get; private set; } = DateTime.Today.AddDays(-7);
    public DateTime To { get; private set; } = DateTime.Today;
    public int CheckNumber { get; private set; }
    public decimal CashAmount { get; private set; }

    public string ShiftNumberText { get; set; } = "";
    public string MarkingCode { get; set; } = "";
    public string MarkingQuantityText { get; set; } = "";
    public MeasureOfQuantity MeasureOfQuantity { get; set; } = MeasureOfQuantity.Piece;
    public string FractionalNumeratorText { get; set; } = "";
    public string FractionalDenominatorText { get; set; } = "";
    public MarkingPlannedStatus PlannedStatus { get; set; } = MarkingPlannedStatus.Sold;
    public bool NotSendToServer { get; set; }
    public bool WaitForResult { get; set; } = true;
    public int ShiftNumber { get; private set; }
    public decimal MarkingQuantity { get; private set; } = 1;
    public int FractionalQuantityNumerator { get; private set; }
    public int FractionalQuantityDenominator { get; private set; }

    /// <summary>
    /// Проверяет поля, которые нужны запросу <paramref name="item"/>, и разбирает числа и даты.
    /// </summary>
    public bool TryRead(Ui.ExampleItem item, out string error)
    {
        if (Address.Trim().Length == 0)
        {
            error = "Укажите адрес сервера ККМ (хост:порт).";
            return false;
        }

        if (item.NeedDocumentId && !item.DocumentIdOptional && DocumentId.Trim().Length == 0)
        {
            error = "Укажите Id документа.";
            return false;
        }

        if (item.NeedFilePath && FilePath.Trim().Length == 0)
        {
            error = "Укажите путь к файлу картинки (BMP/PNG).";
            return false;
        }

        if (item.NeedPool && Pool.Trim().Length == 0)
        {
            error = "Укажите имя пула.";
            return false;
        }

        CheckNumber = 0;
        if (item.NeedCheckNumber)
        {
            if (!int.TryParse(CheckNumberText.Trim(), out var number) || number <= 0)
            {
                error = "Укажите номер фискального документа — целое число больше 0.";
                return false;
            }
            CheckNumber = number;
        }

        // Сумму не валидируем здесь: пустая/нулевая уйдёт на Сервер ККМ, ошибку смотрим в ErrorCode.
        CashAmount = 0;
        if (item.NeedCashAmount && CashAmountText.Trim().Length > 0)
        {
            var text = CashAmountText.Trim().Replace(" ", "").Replace(',', '.');
            if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
            {
                error = "Сумма должна быть числом, напр. 1000 или 150,50.";
                return false;
            }
            CashAmount = amount;
        }

        // Guid обязателен только там, где его передают в запрос (ConfirmKM). После RequestKM поле заполняется само.
        if (item.NeedGuid && !item.GuidOptional && Guid.Trim().Length == 0)
        {
            error = "Укажите Guid из проверки кода маркировки (RequestKM).";
            return false;
        }

        ShiftNumber = 0;
        if (item.NeedShiftNumber)
        {
            if (!int.TryParse(ShiftNumberText.Trim(), out var shift) || shift <= 0)
            {
                error = "Укажите номер смены — целое число больше 0.";
                return false;
            }
            ShiftNumber = shift;
        }

        // Код маркировки и количество не блокируем: пустые значения проверит Сервер ККМ.
        MarkingQuantity = 1;
        if (item.NeedMarkingQuantity && MarkingQuantityText.Trim().Length > 0)
        {
            var text = MarkingQuantityText.Trim().Replace(" ", "").Replace(',', '.');
            if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var quantity))
            {
                error = "Количество должно быть числом, напр. 1 или 0,5.";
                return false;
            }
            MarkingQuantity = quantity;
        }

        FractionalQuantityNumerator = 0;
        FractionalQuantityDenominator = 0;
        if (item.NeedFractionalQuantity)
        {
            if (!TryParseOptionalInt(FractionalNumeratorText, out var numerator)
                || !TryParseOptionalInt(FractionalDenominatorText, out var denominator))
            {
                error = "Числитель и знаменатель дробного количества — целые числа от 0 (или пусто).";
                return false;
            }
            FractionalQuantityNumerator = numerator;
            FractionalQuantityDenominator = denominator;
        }

        if (!TryParseDate(FromText, DateTime.Today.AddDays(-7), "с", out var from, out error))
            return false;
        if (!TryParseDate(ToText, DateTime.Today, "по", out var to, out error))
            return false;
        if (from > to)
        {
            error = "Дата «с» позже даты «по».";
            return false;
        }

        From = from;
        To = to;
        error = "";
        return true;
    }

    private static bool TryParseOptionalInt(string text, out int value)
    {
        value = 0;
        var trimmed = text.Trim();
        return trimmed.Length == 0 || (int.TryParse(trimmed, out value) && value >= 0);
    }

    private static bool TryParseDate(string text, DateTime fallback, string label, out DateTime date, out string error)
    {
        var value = text.Trim();
        if (value.Length == 0)
        {
            date = fallback;
            error = "";
            return true;
        }

        if (DateTime.TryParseExact(value, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            error = "";
            return true;
        }

        error = $"Укажите дату «{label}» в формате дд.мм.гггг.";
        return false;
    }
}
