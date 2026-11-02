using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

/// <summary>
/// Хост, порт и токен подставляются в сессию; имя касса и кассир — в тело запроса в коде примера.
/// </summary>
public class Sample
{
    public SkkmConnector connector { get; set; } = null!;

    /// <summary>Имя ККМ, {{SKKMDEVICE}}.</summary>
    public string deviceName = "";

    /// <summary>Имя кассира из панели подключения.</summary>
    public string cashierName = "";

    /// <summary>ИНН кассира из панели подключения.</summary>
    public string cashierVatin = "";

    /// <summary>Id документа (DocId). Тот же идентификатор в очереди называется taskId.</summary>
    public string documentId = "";

    /// <summary>Начало периода (from) из панели подключения.</summary>
    public DateTime fromDate;

    /// <summary>Конец периода (to) из панели подключения.</summary>
    public DateTime toDate;

    /// <summary>Путь к файлу картинки (BMP/PNG) из панели подключения. Проверку формата делает коннектор.</summary>
    public string filePath = "";

    /// <summary>Имя пула ККТ из панели подключения.</summary>
    public string pool = "";

    /// <summary>Номер фискального документа (ФД) из панели подключения.</summary>
    public int checkNumber;

    /// <summary>Сумма внесения / выемки из панели подключения.</summary>
    public decimal cashAmount;

    /// <summary>Guid из проверки кода маркировки (RequestKM).</summary>
    public string guid = "";

    /// <summary>Признак подтверждения кода маркировки.</summary>
    public KmConfirmationType confirmationType = KmConfirmationType.Included;

    /// <summary>Коды маркировки через запятую.</summary>
    public string codes = "";

    /// <summary>Номер смены из панели подключения.</summary>
    public int shiftNumber;

    /// <summary>Код маркировки в Base64 для проверки (RequestKM).</summary>
    public string markingCode = "";

    /// <summary>Количество товара с кодом маркировки.</summary>
    public decimal markingQuantity = 1;

    /// <summary>Мера количества (таблица 114 ФФД).</summary>
    public MeasureOfQuantity measureOfQuantity = MeasureOfQuantity.Piece;

    /// <summary>Числитель дробного количества.</summary>
    public int fractionalQuantityNumerator;

    /// <summary>Знаменатель дробного количества.</summary>
    public int fractionalQuantityDenominator;

    /// <summary>Планируемый статус товара (таблица 105 ФФД).</summary>
    public MarkingPlannedStatus plannedStatus = MarkingPlannedStatus.Sold;

    /// <summary>Не отправлять результат проверки на сервер ОИСМ (RequestKM).</summary>
    public bool notSendToServer;

    /// <summary>Ждать ответа ОИСМ (RequestKM).</summary>
    public bool waitForResult;
}
