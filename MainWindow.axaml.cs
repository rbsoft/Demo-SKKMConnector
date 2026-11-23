using System;
using System.Diagnostics;
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
    private ComboBox _isProcessed = null!;
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
    private Canvas _docTipLayer = null!;
    private Border _docTip = null!;
    private TextBlock _docText = null!;

    private static readonly IBrush DescriptionBrush = new ImmutableSolidColorBrush(Color.Parse("#7E7E86"));
    private static readonly IBrush StatusOkBrush = new ImmutableSolidColorBrush(Color.Parse("#62C073"));
    private static readonly IBrush StatusErrorBrush = new ImmutableSolidColorBrush(Color.Parse("#E06C75"));
    private static readonly IBrush StatusIdleBrush = new ImmutableSolidColorBrush(Color.Parse("#7E7E86"));

    private ExampleItem? _selected;
    private readonly Dictionary<ExampleItem, (string Response, string Status, IBrush Brush)> _results = new();
    private TreeViewItem? _selectedLeaf;
    private bool _syncingSelection;
    private string _currentSource = "";
    private string _currentResponse = "";
    private string? _lastTip;
    private int _deviceLoadVersion;

    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
        BindControls();

        _measureOfQuantity.ItemsSource = EnumChoices.Measures;
        _measureOfQuantity.SelectedIndex = 0;
        _plannedStatus.ItemsSource = EnumChoices.PlannedStatuses;
        _plannedStatus.SelectedIndex = 0;

        // Даты списков необязательны: пустые from/to не уходят в запрос (Сервер ККМ 4.0.70.1116+).
        _from.SelectedDateChanged += (_, _) => LimitPeriod();
        _to.SelectedDateChanged += (_, _) => LimitPeriod();
        LimitPeriod();

        _device.DropDownOpened += async (_, _) => await LoadDevicesAsync();

        foreach (var root in Ui.Examples.Tree)
            _tree.Items.Add(BuildNode(root));

        _tree.SelectionChanged += OnExampleSelected;
        _source.PointerMoved += (_, e) => OnDocPointerMoved(_source, _currentSource, e);
        _response.PointerMoved += (_, e) => OnDocPointerMoved(_response, _currentResponse, e);
        _source.PointerExited += (_, _) => HideDocTip();
        _response.PointerExited += (_, _) => HideDocTip();
        Closed += (_, _) => Connector.Instance.Dispose();
    }

    private void BindControls()
    {
        _address = C<TextBox>("AddressBox");
        _token = C<TextBox>("TokenBox");
        _device = C<ComboBox>("DeviceBox");
        _cashierPanel = C<Control>("CashierPanel");
        _periodPanel = C<Control>("PeriodPanel");
        _filePanel = C<Control>("FilePanel");
        _pool = C<TextBox>("PoolBox");
        _checkNumber = C<TextBox>("CheckNumberBox");
        _cashAmount = C<TextBox>("CashAmountBox");
        _guid = C<TextBox>("GuidBox");
        _confirmationType = C<ComboBox>("ConfirmationTypeBox");
        _codes = C<TextBox>("CodesBox");
        _shiftNumber = C<TextBox>("ShiftNumberBox");
        _isProcessed = C<ComboBox>("IsProcessedBox");
        _markingCode = C<TextBox>("MarkingCodeBox");
        _markingQuantityPanel = C<Control>("MarkingQuantityPanel");
        _markingQuantity = C<TextBox>("MarkingQuantityBox");
        _measureOfQuantity = C<ComboBox>("MeasureOfQuantityBox");
        _fractionalQuantityPanel = C<Control>("FractionalQuantityPanel");
        _fractionalNumerator = C<TextBox>("FractionalNumeratorBox");
        _fractionalDenominator = C<TextBox>("FractionalDenominatorBox");
        _plannedStatus = C<ComboBox>("PlannedStatusBox");
        _notSendToServer = C<CheckBox>("NotSendToServerBox");
        _waitForResult = C<CheckBox>("WaitForResultBox");
        _cashier = C<TextBox>("CashierBox");
        _cashierVatin = C<TextBox>("CashierVatinBox");
        _documentId = C<TextBox>("DocumentIdBox");
        _filePath = C<TextBox>("FilePathBox");
        _from = C<CalendarDatePicker>("FromBox");
        _to = C<CalendarDatePicker>("ToBox");
        _tree = C<TreeView>("ExamplesTree");
        _run = C<Button>("RunButton");
        _cancel = C<Button>("CancelButton");
        _status = C<TextBlock>("StatusText");
        _source = C<SelectableTextBlock>("SourceText");
        _copy = C<Button>("CopyButton");
        _response = C<SelectableTextBlock>("ResponseBox");
        _requestTitle = C<TextBlock>("RequestTitle");
        _docTipLayer = C<Canvas>("DocTipLayer");
        _docTip = C<Border>("DocTip");
        _docText = C<TextBlock>("DocText");
    }

    private T C<T>(string name) where T : Control => this.FindControl<T>(name)!;

    private void OnDocPointerMoved(SelectableTextBlock box, string text, Avalonia.Input.PointerEventArgs e)
    {
        string? tip = null;
        try
        {
            var pos = e.GetPosition(box);
            pos = new Point(pos.X - box.Padding.Left, pos.Y - box.Padding.Top);
            if (CharIndexAt(box.TextLayout, pos) is int i)
                tip = CSharpHighlighter.SummaryAt(text, i);
        }
        catch
        {
            // без тултипа
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

    private TreeViewItem BuildNode(ExampleTreeNode node)
    {
        if (node.Item != null)
        {
            return new TreeViewItem
            {
                Header = RequestHeader(node.Item),
                Tag = node.Item
            };
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

        bool Any(params string[] fields) => fields.Any(item.Uses);
        void Show(Control control, params string[] fields) => control.IsVisible = Any(fields);

        // Адрес, токен и ККТ видны всегда; остальное — по использованию в примере.
        Show(_cashierPanel, "cashierName", "cashierVatin");
        Show(_documentId, "documentId");
        _documentId.Watermark = item.DocumentIdOptional
            ? "Id документа для повторной печати (необязательно)"
            : "Id документа (DocId / taskId)";
        Show(_periodPanel, "fromDate", "toDate");
        Show(_filePanel, "filePath");
        Show(_pool, "pool");
        Show(_checkNumber, "checkNumber");
        Show(_cashAmount, "cashAmount");
        Show(_guid, "guid");
        _guid.Watermark = item.GuidOptional
            ? "Guid (создаётся при RequestKM)"
            : "Guid из проверки КМ (RequestKM)";
        Show(_confirmationType, "confirmationType");
        Show(_codes, "codes");
        Show(_shiftNumber, "shiftNumber");
        Show(_isProcessed, "isProcessed");
        Show(_markingCode, "markingCode");
        Show(_markingQuantityPanel, "markingQuantity", "measureOfQuantity");
        Show(_markingQuantity, "markingQuantity");
        Show(_measureOfQuantity, "measureOfQuantity");
        Show(_fractionalQuantityPanel, "fractionalQuantityNumerator", "fractionalQuantityDenominator");
        Show(_plannedStatus, "plannedStatus");
        Show(_notSendToServer, "notSendToServer");
        Show(_waitForResult, "waitForResult");

        var (response, status, brush) = _results.GetValueOrDefault(item, ("", "", StatusIdleBrush));
        ShowResponse(response);
        SetStatus(status, brush);
        CSharpHighlighter.Apply(_currentSource, _source.Inlines!);
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

    private void OnAboutClick(object? sender, RoutedEventArgs e)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Assets", "SkkmConnector.pdf");
        if (!File.Exists(path))
            path = Path.Combine(AppContext.BaseDirectory, "SkkmConnector.pdf");
        if (!File.Exists(path))
        {
            SetStatus("PDF не найден", StatusErrorBrush);
            return;
        }

        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }

    private async void OnCopyClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentSource) || Clipboard is not { } clipboard)
            return;

        await clipboard.SetTextAsync(_currentSource);
        ToolTip.SetTip(_copy, "Скопировано");
    }

    private async void OnRunClick(object? sender, RoutedEventArgs e)
    {
        // Пока запрос выполняется, можно перейти на другой пример — ответ запишется своему.
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
            _connection.ApplyTo(item.Instance, Connector.Instance);

            var originalOut = Console.Out;
            var printed = new StringWriter();
            Console.SetOut(printed);
            SkkmConnector kkm;
            try
            {
                kkm = await item.Run();
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            var response = printed.ToString().TrimEnd();

            if (kkm.ErrorCode == 0 && !string.IsNullOrWhiteSpace(kkm.Guid))
                _guid.Text = kkm.Guid;

            if (kkm.ErrorCode == -3)
                SetResult(item, response, "Отменено", StatusIdleBrush);
            else
                SetResult(item, response, kkm.ErrorCode == 0 ? "OK" : "Error",
                    kkm.ErrorCode == 0 ? StatusOkBrush : StatusErrorBrush);
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
        => Connector.Instance.Cancel();

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
    /// «с» — не позже «по» и не позже сегодня; «по» — не раньше «с», будущие даты разрешены.
    /// </summary>
    private void LimitPeriod()
    {
        var today = DateTime.Today;
        var from = _from.SelectedDate;
        var to = _to.SelectedDate;

        // «с»: верхняя граница — сегодня, и не позже выбранного «по» (если «по» раньше сегодня).
        DateTime fromEnd = today;
        if (to.HasValue && to.Value.Date < today)
            fromEnd = to.Value.Date;

        _from.DisplayDateStart = null;
        _from.DisplayDateEnd = fromEnd;
        _to.DisplayDateStart = from;
        _to.DisplayDateEnd = null; // «по» без ограничения на будущие дни
    }

    /// <summary>
    /// Список ККТ с сервера. Отдельный коннектор — чтобы не мешать текущему запросу.
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

        if (version != _deviceLoadVersion)
            return;

        if (error != null)
        {
            ToolTip.SetTip(_device, "Не удалось получить список ККТ: " + error);
            _device.ItemsSource = selected == null ? Array.Empty<string>() : [selected];
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
        _connection.IsProcessed = _isProcessed.SelectedIndex switch
        {
            1 => true,
            2 => false,
            _ => null
        };
        _connection.MarkingCode = _markingCode.Text ?? "";
        _connection.MarkingQuantityText = _markingQuantity.Text ?? "";
        _connection.MeasureOfQuantity =
            (_measureOfQuantity.SelectedItem as EnumChoice<MeasureOfQuantity>)?.Value ?? MeasureOfQuantity.Piece;
        _connection.FractionalNumeratorText = _fractionalNumerator.Text ?? "";
        _connection.FractionalDenominatorText = _fractionalDenominator.Text ?? "";
        _connection.PlannedStatus =
            (_plannedStatus.SelectedItem as EnumChoice<MarkingPlannedStatus>)?.Value ?? MarkingPlannedStatus.Sold;
        _connection.NotSendToServer = _notSendToServer.IsChecked == true;
        _connection.WaitForResult = _waitForResult.IsChecked == true;
        _connection.FromText = DateText(_from);
        _connection.ToText = DateText(_to);
    }

    private static string DateText(CalendarDatePicker picker) =>
        picker.SelectedDate?.ToString(ConnectionSettings.DateFormat) ?? picker.Text ?? "";

    private void SetResult(ExampleItem item, string response, string status, IBrush brush)
    {
        _results[item] = (response, status, brush);
        if (item != _selected)
            return;
        ShowResponse(response);
        SetStatus(status, brush);
    }

    private void ShowResponse(string response)
    {
        _currentResponse = response;
        CSharpHighlighter.ApplyResponse(response, _response.Inlines!);
    }

    private void SetStatus(string text, IBrush brush)
    {
        _status.Text = text;
        _status.Foreground = brush;
    }
}
