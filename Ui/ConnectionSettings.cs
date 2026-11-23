using System.Globalization;
using RBSoftSkkm;
using SkkmNugetSample.Examples;

namespace SkkmNugetSample.Ui;

/// <summary>
/// Значения полей панели подключения в окне демо (не сам коннектор).
/// </summary>
public class ConnectionSettings
{
    public const string DateFormat = "dd.MM.yyyy";
    private static readonly string[] DateFormats = [DateFormat, "yyyy-MM-dd", "d.M.yyyy"];

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
    /// <summary>null — from не передавать в коннектор / запрос.</summary>
    public DateTime? From { get; private set; }
    /// <summary>null — to не передавать в коннектор / запрос.</summary>
    public DateTime? To { get; private set; }
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
    /// <summary>null — isProcessed не передавать.</summary>
    public bool? IsProcessed { get; set; }
    public decimal MarkingQuantity { get; private set; } = 1;
    public int FractionalQuantityNumerator { get; private set; }
    public int FractionalQuantityDenominator { get; private set; }

    /// <summary>
    /// Проверяет поля, которые нужны запросу <paramref name="item"/>, и разбирает числа и даты.
    /// </summary>
    public bool TryRead(ExampleItem item, out string error)
    {
        if (Address.Trim().Length == 0)
        {
            error = "Укажите адрес сервера ККМ (хост:порт).";
            return false;
        }

        if (item.Uses("documentId") && !item.DocumentIdOptional && DocumentId.Trim().Length == 0)
        {
            error = "Укажите Id документа.";
            return false;
        }

        if (item.Uses("filePath") && FilePath.Trim().Length == 0)
        {
            error = "Укажите путь к файлу картинки (BMP/PNG).";
            return false;
        }

        if (item.Uses("pool") && Pool.Trim().Length == 0)
        {
            error = "Укажите имя пула.";
            return false;
        }

        CheckNumber = 0;
        if (item.Uses("checkNumber"))
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
        if (item.Uses("cashAmount") && CashAmountText.Trim().Length > 0)
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
        if (item.Uses("guid") && !item.GuidOptional && Guid.Trim().Length == 0)
        {
            error = "Укажите Guid из проверки кода маркировки (RequestKM).";
            return false;
        }

        // shift необязателен для списков: пусто — не передаём.
        ShiftNumber = 0;
        if (item.Uses("shiftNumber") && ShiftNumberText.Trim().Length > 0)
        {
            if (!int.TryParse(ShiftNumberText.Trim(), out var shift) || shift <= 0)
            {
                error = "Номер смены — целое число больше 0 (или пусто, чтобы не фильтровать).";
                return false;
            }
            ShiftNumber = shift;
        }

        // Код маркировки и количество не блокируем: пустые значения проверит Сервер ККМ.
        MarkingQuantity = 1;
        if (item.Uses("markingQuantity") && MarkingQuantityText.Trim().Length > 0)
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
        if (item.Uses("fractionalQuantityNumerator") || item.Uses("fractionalQuantityDenominator"))
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

        // from/to независимо необязательны (changelog 4.0.70.1116): пусто — параметр не уходит.
        From = null;
        To = null;
        if (item.Uses("fromDate") || item.Uses("toDate"))
        {
            if (!TryParseOptionalDate(FromText, "с", out var from, out error))
                return false;
            if (!TryParseOptionalDate(ToText, "по", out var to, out error))
                return false;
            if (from is { } f && to is { } t && f > t)
            {
                error = "Дата «с» позже даты «по».";
                return false;
            }
            From = from;
            To = to;
        }

        error = "";
        return true;
    }

    /// <summary>
    /// Подставляет параметры панели в пример и в коннектор.
    /// </summary>
    public void ApplyTo(Sample sample, SkkmConnector connector)
    {
        connector.Clear();
        connector.Address = Address.Trim();
        connector.Token = Token.Trim();
        connector.FromDate = From ?? default;
        connector.ToDate = To ?? default;
        connector.ListIsProcessed = IsProcessed;

        sample.connector = connector;
        sample.deviceName = Device.Trim();
        sample.cashierName = Cashier.Trim();
        sample.cashierVatin = CashierVatin.Trim();
        sample.documentId = DocumentId.Trim();
        sample.filePath = FilePath.Trim();
        sample.pool = Pool.Trim();
        sample.checkNumber = CheckNumber;
        sample.cashAmount = CashAmount;
        sample.guid = Guid.Trim();
        sample.confirmationType = ConfirmationType;
        sample.codes = Codes.Trim();
        sample.shiftNumber = ShiftNumber;
        sample.isProcessed = IsProcessed;
        sample.markingCode = MarkingCode.Trim();
        sample.markingQuantity = MarkingQuantity;
        sample.measureOfQuantity = MeasureOfQuantity;
        sample.fractionalQuantityNumerator = FractionalQuantityNumerator;
        sample.fractionalQuantityDenominator = FractionalQuantityDenominator;
        sample.plannedStatus = PlannedStatus;
        sample.notSendToServer = NotSendToServer;
        sample.waitForResult = WaitForResult;
        sample.fromDate = From ?? default;
        sample.toDate = To ?? default;
    }

    private static bool TryParseOptionalInt(string text, out int value)
    {
        value = 0;
        var trimmed = text.Trim();
        return trimmed.Length == 0 || (int.TryParse(trimmed, out value) && value >= 0);
    }

    /// <summary>Пустая строка — null (параметр не уйдёт в запрос).</summary>
    private static bool TryParseOptionalDate(string text, string label, out DateTime? date, out string error)
    {
        var value = text.Trim();
        if (value.Length == 0)
        {
            date = null;
            error = "";
            return true;
        }

        if (DateTime.TryParseExact(value, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
        {
            date = parsed;
            error = "";
            return true;
        }

        date = null;
        error = $"Укажите дату «{label}» в формате дд.мм.гггг (или оставьте пустым).";
        return false;
    }
}
