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
    private TextBox _device = null!;
    private TextBox _cashier = null!;
    private TextBox _cashierVatin = null!;
    private TextBox _documentId = null!;
    private TextBox _filePath = null!;
    private TextBox _from = null!;
    private TextBox _to = null!;
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
        _device = this.FindControl<TextBox>("DeviceBox")!;
        _cashier = this.FindControl<TextBox>("CashierBox")!;
        _cashierVatin = this.FindControl<TextBox>("CashierVatinBox")!;
        _documentId = this.FindControl<TextBox>("DocumentIdBox")!;
        _filePath = this.FindControl<TextBox>("FilePathBox")!;
        _from = this.FindControl<TextBox>("FromBox")!;
        _to = this.FindControl<TextBox>("ToBox")!;
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

        _from.Text = DateTime.Today.AddDays(-7).ToString(ConnectionSettings.DateFormat);
        _to.Text = DateTime.Today.ToString(ConnectionSettings.DateFormat);

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
        SetResponse("");
        SetStatus("", StatusIdleBrush);

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
        if (_selected == null)
        {
            SetStatus("Выберите пример", StatusIdleBrush);
            return;
        }

        ReadConnection();
        if (!_connection.TryRead(_selected.NeedDocumentId, _selected.NeedFilePath, out string error))
        {
            SetResponse(error);
            SetStatus("Проверьте подключение", StatusIdleBrush);
            return;
        }

        _run.IsEnabled = false;
        _cancel.IsEnabled = true;
        SetStatus("Выполняется…", StatusIdleBrush);

        try
        {
            ExampleRunner.ApplyConnection(_selected.Instance, _connection);

            // Перехватываем вывод примера (Console.WriteLine), чтобы показать в панели
            // именно то, что напечатал код примера.
            var originalOut = Console.Out;
            var printed = new StringWriter();
            Console.SetOut(printed);
            SkkmConnector kkm;
            try
            {
                kkm = await ExampleRunner.Invoke(_selected);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            // Показываем только то, что напечатал сам пример (Console.WriteLine).
            // Если пример ничего не печатает — панель пустая.
            SetResponse(printed.ToString().TrimEnd());
            if (kkm.ErrorCode == -3)
                SetStatus("Отменено", StatusIdleBrush);
            else
                SetStatus(kkm.Ok ? "OK" : "Error", kkm.Ok ? StatusOkBrush : StatusErrorBrush);
        }
        catch (Exception ex)
        {
            SetResponse(ex.ToString());
            SetStatus("Error", StatusErrorBrush);
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

    private void ReadConnection()
    {
        _connection.Address = _address.Text ?? "";
        _connection.Token = _token.Text ?? "";
        _connection.Device = _device.Text ?? "";
        _connection.Cashier = _cashier.Text ?? "";
        _connection.CashierVatin = _cashierVatin.Text ?? "";
        _connection.DocumentId = _documentId.Text ?? "";
        _connection.FilePath = _filePath.Text ?? "";
        _connection.FromText = _from.Text ?? "";
        _connection.ToText = _to.Text ?? "";
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
