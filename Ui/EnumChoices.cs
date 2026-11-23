using RBSoftSkkm;

namespace SkkmNugetSample.Ui;

/// <summary>Пункт выпадающего списка: значение перечисления и подпись.</summary>
public sealed record EnumChoice<T>(T Value, string Label) where T : struct, Enum
{
    public override string ToString() => $"{Label} ({Value})";
}

public static class EnumChoices
{
    public static readonly EnumChoice<MeasureOfQuantity>[] Measures =
    [
        new(MeasureOfQuantity.Piece, "Штука"),
        new(MeasureOfQuantity.Gram, "Грамм"),
        new(MeasureOfQuantity.Kilogram, "Килограмм"),
        new(MeasureOfQuantity.Tonne, "Тонна"),
        new(MeasureOfQuantity.Centimeter, "Сантиметр"),
        new(MeasureOfQuantity.Decimeter, "Дециметр"),
        new(MeasureOfQuantity.Meter, "Метр"),
        new(MeasureOfQuantity.SquareCentimeter, "Кв. сантиметр"),
        new(MeasureOfQuantity.SquareDecimeter, "Кв. дециметр"),
        new(MeasureOfQuantity.SquareMeter, "Кв. метр"),
        new(MeasureOfQuantity.Milliliter, "Миллилитр"),
        new(MeasureOfQuantity.Liter, "Литр"),
        new(MeasureOfQuantity.CubicMeter, "Куб. метр"),
        new(MeasureOfQuantity.KilowattHour, "Киловатт-час"),
        new(MeasureOfQuantity.Gigacalorie, "Гигакалория"),
        new(MeasureOfQuantity.Day, "Сутки"),
        new(MeasureOfQuantity.Hour, "Час"),
        new(MeasureOfQuantity.Minute, "Минута"),
        new(MeasureOfQuantity.Second, "Секунда"),
        new(MeasureOfQuantity.Kilobyte, "Килобайт"),
        new(MeasureOfQuantity.Megabyte, "Мегабайт"),
        new(MeasureOfQuantity.Gigabyte, "Гигабайт"),
        new(MeasureOfQuantity.Terabyte, "Терабайт"),
        new(MeasureOfQuantity.Other, "Иная единица"),
    ];

    public static readonly EnumChoice<MarkingPlannedStatus>[] PlannedStatuses =
    [
        new(MarkingPlannedStatus.Sold, "Реализован"),
        new(MarkingPlannedStatus.InSale, "Мерный товар в реализации"),
        new(MarkingPlannedStatus.Returned, "Возвращён"),
        new(MarkingPlannedStatus.PartiallyReturned, "Часть товара возвращена"),
        new(MarkingPlannedStatus.Unchanged, "Статус не изменился"),
        new(MarkingPlannedStatus.NotSpecified, "Не задан"),
    ];
}
