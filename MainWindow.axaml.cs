using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Media.TextFormatting;
using Avalonia.Platform.Storage;
using RBSoftSkkm;
using SkkmNugetSample.Ui;

namespace SkkmNugetSample;

public partial class MainWindow : Window
{
    private readonly ConnectionSettings _connection = new();

    private TextBox _address = null!;
    private TextBox _token = null!;
    private ComboBox _device = null!;
    private Control _cashierPanel = null!;
    private Control _periodPanel = null!;
    private Control _filePanel = null!;
    private TextBox _pool = null!;
    private TextBox _checkNumber = null!;
    private TextBox _cashAmount = null!;
    private TextBox _guid = null!;
    private ComboBox _confirmationType = null!;
    private TextBox _codes = null!;
    private TextBox _shiftNumber = null!;
    private TextBox _markingCode = null!;
    private Control _markingQuantityPanel = null!;
    private TextBox _markingQuantity = null!;
    private ComboBox _measureOfQuantity = null!;
    private Control _fractionalQuantityPanel = null!;
    private TextBox _fractionalNumerator = null!;
    private TextBox _fractionalDenominator = null!;
    private ComboBox _plannedStatus = null!;
    private CheckBox _notSendToServer = null!;
    private CheckBox _waitForResult = null!;
    private TextBox _cashier = null!;
    private TextBox _cashierVatin = null!;
    private TextBox _documentId = null!;
    private TextBox _filePath = null!;
    private CalendarDatePicker _from = null!;
    private CalendarDatePicker _to = null!;
    private TreeView _tree = null!;
    private Button _run = null!;
    private Button _cancel = null!;
    private TextBlock _status = null!;
    private SelectableTextBlock _source = null!;
    private Button _copy = null!;
    private SelectableTextBlock _response = null!;
    private TextBlock _requestTitle = null!;

    private static readonly IBrush DescriptionBrush = new ImmutableSolidColorBrush(Color.Parse("#7E7E86"));
    private static readonly IBrush StatusOkBrush = new ImmutableSolidColorBrush(Color.Parse("#62C073"));
    private static readonly IBrush StatusErrorBrush = new ImmutableSolidColorBrush(Color.Parse("#E06C75"));
    private static readonly IBrush StatusIdleBrush = new ImmutableSolidColorBrush(Color.Parse("#7E7E86"));

    private ExampleItem? _selected;

    // Последний ответ и статус каждого примера: при переходе между запросами ответ не теряется (до закрытия приложения).
    private readonly Dictionary<ExampleItem, (string Response, string Status, IBrush Brush)> _results = new();
    private TreeViewItem? _selectedLeaf;
    private bool _syncingSelection;
    private string _currentSource = "";
    private string? _lastTip;
    private Canvas _docTipLayer = null!;
    private Border _docTip = null!;
    private TextBlock _docText = null!;

    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);

        _address = this.FindControl<TextBox>("AddressBox")!;
        _token = this.FindControl<TextBox>("TokenBox")!;
        _device = this.FindControl<ComboBox>("DeviceBox")!;
        _cashierPanel = this.FindControl<Control>("CashierPanel")!;
        _periodPanel = this.FindControl<Control>("PeriodPanel")!;
        _filePanel = this.FindControl<Control>("FilePanel")!;
        _pool = this.FindControl<TextBox>("PoolBox")!;
        _checkNumber = this.FindControl<TextBox>("CheckNumberBox")!;
        _cashAmount = this.FindControl<TextBox>("CashAmountBox")!;
        _guid = this.FindControl<TextBox>("GuidBox")!;
        _confirmationType = this.FindControl<ComboBox>("ConfirmationTypeBox")!;
        _codes = this.FindControl<TextBox>("CodesBox")!;
        _shiftNumber = this.FindControl<TextBox>("ShiftNumberBox")!;
        _markingCode = this.FindControl<TextBox>("MarkingCodeBox")!;
        _markingQuantityPanel = this.FindControl<Control>("MarkingQuantityPanel")!;
        _markingQuantity = this.FindControl<TextBox>("MarkingQuantityBox")!;
        _measureOfQuantity = this.FindControl<ComboBox>("MeasureOfQuantityBox")!;
        _fractionalQuantityPanel = this.FindControl<Control>("FractionalQuantityPanel")!;
        _fractionalNumerator = this.FindControl<TextBox>("FractionalNumeratorBox")!;
        _fractionalDenominator = this.FindControl<TextBox>("FractionalDenominatorBox")!;
        _plannedStatus = this.FindControl<ComboBox>("PlannedStatusBox")!;
        _notSendToServer = this.FindControl<CheckBox>("NotSendToServerBox")!;
        _waitForResult = this.FindControl<CheckBox>("WaitForResultBox")!;
        FillEnumChoices();
        _cashier = this.FindControl<TextBox>("CashierBox")!;
        _cashierVatin = this.FindControl<TextBox>("CashierVatinBox")!;
        _documentId = this.FindControl<TextBox>("DocumentIdBox")!;
        _filePath = this.FindControl<TextBox>("FilePathBox")!;
        _from = this.FindControl<CalendarDatePicker>("FromBox")!;
        _to = this.FindControl<CalendarDatePicker>("ToBox")!;
        _tree = this.FindControl<TreeView>("ExamplesTree")!;
        _run = this.FindControl<Button>("RunButton")!;
        _cancel = this.FindControl<Button>("CancelButton")!;
        _status = this.FindControl<TextBlock>("StatusText")!;
        _source = this.FindControl<SelectableTextBlock>("SourceText")!;
        _copy = this.FindControl<Button>("CopyButton")!;
        _response = this.FindControl<SelectableTextBlock>("ResponseBox")!;
        _requestTitle = this.FindControl<TextBlock>("RequestTitle")!;
        _docTipLayer = this.FindControl<Canvas>("DocTipLayer")!;
        _docTip = this.FindControl<Border>("DocTip")!;
        _docText = this.FindControl<TextBlock>("DocText")!;

        _from.SelectedDate = DateTime.Today.AddDays(-7);
        _to.SelectedDate = DateTime.Today;
        _from.SelectedDateChanged += (_, _) => LimitPeriod();
        _to.SelectedDateChanged += (_, _) => LimitPeriod();
        LimitPeriod();

        _device.DropDownOpened += async (_, _) => await LoadDevicesAsync();

        LoadExamples();
        _tree.SelectionChanged += OnExampleSelected;
        _source.PointerMoved += OnSourcePointerMoved;
        _source.PointerExited += (_, _) => HideDocTip();
        Closed += (_, _) => ExampleRunner.Session.Dispose();
    }

    // Тултип с <summary> типа/поля под курсором 
    private void OnSourcePointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        string? tip = null;
        try
        {
            var pos = e.GetPosition(_source);
            pos = new Point(pos.X - _source.Padding.Left, pos.Y - _source.Padding.Top);
            int? index = CharIndexAt(_source.TextLayout, pos);
            if (index is int i)
                tip = CSharpHighlighter.SummaryAt(_currentSource, i);
        }
        catch
        {
            // не удалось определить позицию — без тултипа
        }

        if (tip == _lastTip)
        {
            if (tip != null)
                PlaceDocTip(e.GetPosition(_docTipLayer));
            return;
        }
        _lastTip = tip;

        if (tip is null)
        {
            HideDocTip();
            return;
        }

        _docText.Text = tip;
        _docTip.IsVisible = true;
        PlaceDocTip(e.GetPosition(_docTipLayer));
    }

    private static int? CharIndexAt(TextLayout layout, Point pos)
    {
        if (pos.Y < 0)
            return null;

        double y = 0;
        foreach (var line in layout.TextLines)
        {
            double next = y + line.Height;
            if (pos.Y < next)
            {
                if (pos.X < 0 || pos.X > line.WidthIncludingTrailingWhitespace)
                    return null;
                return line.GetCharacterHitFromDistance(pos.X).FirstCharacterIndex;
            }
            y = next;
        }
        return null;
    }

    private void PlaceDocTip(Point layerPos)
    {
        _docTip.InvalidateMeasure();
        _docTip.Measure(Size.Infinity);
        var size = _docTip.DesiredSize;
        var bounds = _docTipLayer.Bounds;
        double x = layerPos.X + 14;
        double y = layerPos.Y + 18;
        if (bounds.Width > 0 && x + size.Width > bounds.Width)
            x = Math.Max(0, bounds.Width - size.Width - 4);
        if (bounds.Height > 0 && y + size.Height > bounds.Height)
            y = Math.Max(0, layerPos.Y - size.Height - 8);
        Canvas.SetLeft(_docTip, x);
        Canvas.SetTop(_docTip, y);
    }

    private void HideDocTip()
    {
        _lastTip = null;
        _docTip.IsVisible = false;
    }

    // Заполняет дерево: папки и запросы, как в Bruno.
    private void LoadExamples()
    {
        foreach (var root in Ui.Examples.Tree)
            _tree.Items.Add(BuildNode(root));
    }

    private TreeViewItem BuildNode(ExampleTreeNode node)
    {
        if (node.Item != null)
        {
            var leaf = new TreeViewItem
            {
                Header = RequestHeader(node.Item),
                Tag = node.Item
            };
            leaf.Classes.Add("request");
            return leaf;
        }

        var folder = new TreeViewItem
        {
            Header = FolderHeader(node.Title),
            IsExpanded = true,
            Focusable = false
        };
        folder.Classes.Add("folder");
        foreach (var child in node.Children)
            folder.Items.Add(BuildNode(child));
        return folder;
    }

    private void OnExampleSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (_syncingSelection)
            return;

        if (_tree.SelectedItem is not TreeViewItem node || node.Tag is not ExampleItem item)
        {
            RestoreLeafSelection();
            return;
        }

        _selectedLeaf = node;
        ShowExample(item);
    }

    private void RestoreLeafSelection()
    {
        _syncingSelection = true;
        try
        {
            if (_tree.SelectedItem is TreeViewItem folder)
                folder.IsSelected = false;
            if (_selectedLeaf != null)
                _selectedLeaf.IsSelected = true;
        }
        finally
        {
            _syncingSelection = false;
        }
    }

    private void ShowExample(ExampleItem item)
    {
        _selected = item;
        _currentSource = ExampleSource.Read(item);
        HideDocTip();
        ToolTip.SetTip(_copy, "Копировать");
        _requestTitle.Text = item.MethodName + "()";
        // Адрес, токен и ККТ видны всегда, остальные поля — только если они нужны запросу.
        _cashierPanel.IsVisible = item.NeedCashier;
        _documentId.IsVisible = item.NeedDocumentId;
        _documentId.Watermark = item.DocumentIdOptional
            ? "Id документа для повторной печати (необязательно)"
            : "Id документа (DocId / taskId)";
        _periodPanel.IsVisible = item.NeedPeriod;
        _filePanel.IsVisible = item.NeedFilePath;
        _pool.IsVisible = item.NeedPool;
        _checkNumber.IsVisible = item.NeedCheckNumber;
        _cashAmount.IsVisible = item.NeedCashAmount;
        _guid.IsVisible = item.NeedGuid;
        _guid.Watermark = item.GuidOptional
            ? "Guid (создаётся при RequestKM)"
            : "Guid из проверки КМ (RequestKM)";
        _confirmationType.IsVisible = item.NeedConfirmationType;
        _codes.IsVisible = item.NeedCodes;
        _shiftNumber.IsVisible = item.NeedShiftNumber;
        _markingCode.IsVisible = item.NeedMarkingCode;
        _markingQuantityPanel.IsVisible = item.NeedMarkingQuantity || item.NeedMeasureOfQuantity;
        _markingQuantity.IsVisible = item.NeedMarkingQuantity;
        _measureOfQuantity.IsVisible = item.NeedMeasureOfQuantity;
        _fractionalQuantityPanel.IsVisible = item.NeedFractionalQuantity;
        _plannedStatus.IsVisible = item.NeedPlannedStatus;
        _notSendToServer.IsVisible = item.NeedNotSendToServer;
        _waitForResult.IsVisible = item.NeedWaitForResult;

        var (response, status, brush) = _results.GetValueOrDefault(item, ("", "", StatusIdleBrush));
        SetResponse(response);
        SetStatus(status, brush);

        var inlines = _source.Inlines;
        if (inlines != null)
            CSharpHighlighter.Apply(_currentSource, inlines);
        else
            _source.Text = _currentSource;
    }

    private static Control FolderHeader(string title) =>
        new TextBlock
        {
            Text = title,
            FontWeight = FontWeight.SemiBold,
            VerticalAlignment = VerticalAlignment.Center
        };

    private static Control RequestHeader(ExampleItem item) =>
        new StackPanel
        {
            Orientation = Orientation.Vertical,
            VerticalAlignment = VerticalAlignment.Center,
            Children =
            {
                new TextBlock
                {
                    Text = item.MethodName + "()",
                    TextTrimming = TextTrimming.CharacterEllipsis
                },
                new TextBlock
                {
                    Text = item.Title,
                    FontSize = 14,
                    Foreground = DescriptionBrush,
                    TextTrimming = TextTrimming.CharacterEllipsis
                }
            }
        };

    private async void OnCopyClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentSource))
            return;

        var clipboard = Clipboard;
        if (clipboard == null)
            return;

        await clipboard.SetTextAsync(_currentSource);
        ToolTip.SetTip(_copy, "Скопировано");
    }

    private async void OnRunClick(object? sender, RoutedEventArgs e)
    {
        // Пример запоминаем: пока запрос выполняется, можно перейти на другой — ответ запишется своему примеру.
        var item = _selected;
        if (item == null)
        {
            SetStatus("Выберите пример", StatusIdleBrush);
            return;
        }

        ReadConnection();
        if (!_connection.TryRead(item, out string error))
        {
            SetResult(item, error, "Проверьте подключение", StatusIdleBrush);
            return;
        }

        _run.IsEnabled = false;
        _cancel.IsEnabled = true;
        SetResult(item, "", "Выполняется…", StatusIdleBrush);

        try
        {
            ExampleRunner.ApplyConnection(item.Instance, _connection);

            // Перехватываем вывод примера (Console.WriteLine), чтобы показать в панели
            // именно то, что напечатал код примера.
            var originalOut = Console.Out;
            var printed = new StringWriter();
            Console.SetOut(printed);
            SkkmConnector kkm;
            try
            {
                kkm = await ExampleRunner.Invoke(item);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            // Показываем только то, что напечатал сам пример (Console.WriteLine).
            // Если пример ничего не печатает — панель пустая.
            var response = printed.ToString().TrimEnd();

            // Guid из ответа (RequestKM) сразу подставляется в поле — для ConfirmKM.
            if (kkm.ErrorCode == 0 && !string.IsNullOrWhiteSpace(kkm.Guid))
                _guid.Text = kkm.Guid;

            if (kkm.ErrorCode == -3)
                SetResult(item, response, "Отменено", StatusIdleBrush);
            else
                SetResult(item, response, kkm.ErrorCode == 0 ? "OK" : "Error", kkm.ErrorCode == 0 ? StatusOkBrush : StatusErrorBrush);
        }
        catch (Exception ex)
        {
            SetResult(item, ex.ToString(), "Error", StatusErrorBrush);
        }
        finally
        {
            _run.IsEnabled = true;
            _cancel.IsEnabled = false;
        }
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        ExampleRunner.Session.Cancel();
    }

    private async void OnBrowseFileClick(object? sender, RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Выберите картинку (BMP/PNG)",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("Картинки (BMP, PNG)")
                {
                    Patterns = ["*.bmp", "*.png"]
                }
            ]
        });

        if (files.Count > 0)
            _filePath.Text = files[0].TryGetLocalPath() ?? files[0].Path.LocalPath;
    }

    /// <summary>
    /// Ограничивает календари: «с» — не позже «по», «по» — не раньше «с»; обе даты — не позже сегодняшнего дня.
    /// </summary>
    private void LimitPeriod()
    {
        var today = DateTime.Today;
        var from = _from.SelectedDate;
        var to = _to.SelectedDate;

        _from.DisplayDateStart = null;
        _from.DisplayDateEnd = to ?? today;
        _to.DisplayDateStart = from;
        _to.DisplayDateEnd = today;
    }

    /// <summary>Пункт выпадающего списка: значение перечисления и подпись «Русское название (Имя)».</summary>
    private sealed record EnumChoice<T>(T Value, string Label) where T : struct, Enum
    {
        public override string ToString() => $"{Label} ({Value})";
    }

    private static readonly EnumChoice<MeasureOfQuantity>[] MeasureChoices =
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

    private static readonly EnumChoice<MarkingPlannedStatus>[] PlannedStatusChoices =
    [
        new(MarkingPlannedStatus.Sold, "Реализован"),
        new(MarkingPlannedStatus.InSale, "Мерный товар в реализации"),
        new(MarkingPlannedStatus.Returned, "Возвращён"),
        new(MarkingPlannedStatus.PartiallyReturned, "Часть товара возвращена"),
        new(MarkingPlannedStatus.Unchanged, "Статус не изменился"),
        new(MarkingPlannedStatus.NotSpecified, "Не задан"),
    ];

    private void FillEnumChoices()
    {
        _measureOfQuantity.ItemsSource = MeasureChoices;
        _measureOfQuantity.SelectedIndex = 0;
        _plannedStatus.ItemsSource = PlannedStatusChoices;
        _plannedStatus.SelectedIndex = 0;
    }

    private int _deviceLoadVersion;

    /// <summary>
    /// Запрашивает список ККТ (GetDeviceList) по текущему адресу и токену и заполняет выпадающий список.
    /// Отдельный коннектор — чтобы не мешать запросу, который сейчас выполняется.
    /// </summary>
    private async Task LoadDevicesAsync()
    {
        int version = ++_deviceLoadVersion;
        var selected = _device.SelectedItem as string;

        using var kkm = new SkkmConnector
        {
            Address = (_address.Text ?? "").Trim(),
            Token = (_token.Text ?? "").Trim()
        };

        string? error = null;
        try
        {
            if (kkm.Address.Length == 0)
                error = "Укажите адрес сервера ККМ.";
            else if (!await kkm.GetDeviceList())
                error = $"Код ошибки: {kkm.ErrorCode} {kkm.ErrorDescription}";
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }

        // Пока ждали ответ, список могли открыть ещё раз — применяем только последний результат.
        if (version != _deviceLoadVersion)
            return;

        if (error != null)
        {
            ToolTip.SetTip(_device, "Не удалось получить список ККТ: " + error);
            _device.ItemsSource = selected == null ? Array.Empty<string>() : new[] { selected };
            _device.SelectedItem = selected;
            return;
        }

        ToolTip.SetTip(_device, "Нажмите, чтобы получить список ККТ с сервера");
        var names = kkm.DeviceList.Select(d => d.DeviceName).ToArray();
        _device.ItemsSource = names;
        _device.SelectedItem = names.FirstOrDefault(n => n == selected);
    }

    private void ReadConnection()
    {
        _connection.Address = _address.Text ?? "";
        _connection.Token = _token.Text ?? "";
        _connection.Device = _device.SelectedItem as string ?? "";
        _connection.Cashier = _cashier.Text ?? "";
        _connection.CashierVatin = _cashierVatin.Text ?? "";
        _connection.DocumentId = _documentId.Text ?? "";
        _connection.FilePath = _filePath.Text ?? "";
        _connection.Pool = _pool.Text ?? "";
        _connection.CheckNumberText = _checkNumber.Text ?? "";
        _connection.CashAmountText = _cashAmount.Text ?? "";
        _connection.Guid = _guid.Text ?? "";
        _connection.ConfirmationType = _confirmationType.SelectedIndex == 1
            ? KmConfirmationType.NotIncluded
            : KmConfirmationType.Included;
        _connection.Codes = _codes.Text ?? "";
        _connection.ShiftNumberText = _shiftNumber.Text ?? "";
        _connection.MarkingCode = _markingCode.Text ?? "";
        _connection.MarkingQuantityText = _markingQuantity.Text ?? "";
        _connection.MeasureOfQuantity = (_measureOfQuantity.SelectedItem as EnumChoice<MeasureOfQuantity>)?.Value ?? MeasureOfQuantity.Piece;
        _connection.FractionalNumeratorText = _fractionalNumerator.Text ?? "";
        _connection.FractionalDenominatorText = _fractionalDenominator.Text ?? "";
        _connection.PlannedStatus = (_plannedStatus.SelectedItem as EnumChoice<MarkingPlannedStatus>)?.Value ?? MarkingPlannedStatus.Sold;
        _connection.NotSendToServer = _notSendToServer.IsChecked == true;
        _connection.WaitForResult = _waitForResult.IsChecked == true;
        _connection.FromText = DateText(_from);
        _connection.ToText = DateText(_to);
    }

    // Дата из календаря, а если введённый текст не распознан — сам текст (его проверит ConnectionSettings).
    private static string DateText(CalendarDatePicker picker) =>
        picker.SelectedDate?.ToString(ConnectionSettings.DateFormat) ?? picker.Text ?? "";

    /// <summary>
    /// Запоминает ответ примера и показывает его, если этот пример сейчас открыт.
    /// </summary>
    private void SetResult(ExampleItem item, string response, string status, IBrush brush)
    {
        _results[item] = (response, status, brush);
        if (item != _selected)
            return;
        SetResponse(response);
        SetStatus(status, brush);
    }

    private void SetResponse(string text)
    {
        var inlines = _response.Inlines;
        if (inlines != null)
            CSharpHighlighter.ApplyJson(text, inlines);
        else
            _response.Text = text;
    }

    private void SetStatus(string text, IBrush brush)
    {
        _status.Text = text;
        _status.Foreground = brush;
    }
}
