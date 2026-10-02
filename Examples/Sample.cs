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
}
