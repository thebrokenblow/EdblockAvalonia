using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Edblock.ViewModel.Components;
using Edblock.ViewModel.SymbolsVM;

namespace Edblock.View.Views
{
    public partial class MainWindow : Window
    {
        private EditorVm? _editorVm;
        private Point _ghostPosition = new(0, 0);
        private readonly Point _mouseOffset = new(-5, -5);
        private const string DraggingSymbol = "dragging-symbol";
        private readonly Dictionary<string, Control> _symbolUiByName;
        private Control? _draggedSymbolUi;

        public MainWindow()
        {
            InitializeComponent();

            MenuSymbols.ActionSymbolUi.MainWindow = this;
            MenuSymbols.ConditionSymbolUi.MainWindow = this;

            AddHandler(DragDrop.DragOverEvent, DragOver);
            AddHandler(DragDrop.DropEvent, Drop);

            _symbolUiByName = GetСonfigure();
        }
        
        private Dictionary<string, Control> GetСonfigure() =>
            new()
            {
                { ActionSymbol.Name!, ActionSymbol },
                { ConditionSymbol.Name!, ConditionSymbol }
            };

        public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is not Shape shape) return;

            var tag = shape.Tag?.ToString();
            if (tag == null || !_symbolUiByName.TryGetValue(tag, out var symbolUi)) return;

            _draggedSymbolUi = symbolUi;

            var ghostPos = _draggedSymbolUi.Bounds.Position;
            _ghostPosition = new Point(ghostPos.X + _mouseOffset.X, ghostPos.Y + _mouseOffset.Y);

            var mousePos = e.GetPosition(this);
            var offsetX = mousePos.X - ghostPos.X;
            var offsetY = mousePos.Y - ghostPos.Y + _mouseOffset.X;
            _draggedSymbolUi.RenderTransform = new TranslateTransform(offsetX, offsetY);

            var symbol = new ConditionSymbolVm(_editorVm);
            _draggedSymbolUi.IsVisible = true;

            var dragData = new DataObject();
            dragData.Set(DraggingSymbol, symbol);
            DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
            _draggedSymbolUi.IsVisible = false;
        }

        private void DragOver(object? sender, DragEventArgs e)
        {
            if (_draggedSymbolUi == null) return;

            var currentPosition = e.GetPosition(this);

            var offsetX = currentPosition.X - _ghostPosition.X;
            var offsetY = currentPosition.Y - _ghostPosition.Y;

            _draggedSymbolUi.RenderTransform = new TranslateTransform(offsetX, offsetY);

            e.DragEffects = DragDropEffects.Move;

            var data = (SymbolVm)e.Data.Get(DraggingSymbol)!;
            if ((e.Source as Control)?.Name != "CanvasSymbols")
            {
                e.DragEffects = DragDropEffects.None;
            }
        }

        private void Drop(object? sender, DragEventArgs e)
        {
            if (_editorVm is null || _draggedSymbolUi == null) return;

            var data = e.Data.Get(DraggingSymbol);
            var symbol = (SymbolVm)data;

            var point = e.GetPosition(this);

            symbol.X = point.X - 200 + 7;
            symbol.Y = point.Y + 7;

            if (_editorVm.Symbols is null)
            {
                _editorVm.Symbols = new ObservableCollection<SymbolVm> { symbol };
            }
            else
            {
                _editorVm.Symbols.Add(symbol);
            }

            _draggedSymbolUi.RenderTransform = null;
        }

        private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_editorVm is null && DataContext is EditorVm editorVm)
            {
                _editorVm ??= editorVm;
            }

            var point = e.GetCurrentPoint(sender as Control);

            var x = point.Position.X;
            var y = point.Position.Y;

            _editorVm?.ChangeCoordinateSelectedSymbol(x, y);
        }

        private void SelectSymbolHandler(object? sender, PointerPressedEventArgs e)
        {
            if (sender is UserControl { DataContext: SymbolVm symbolVm })
            {
                symbolVm.SetSelectedProperties();
            }

            e.Handled = true;
        }

        private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _editorVm?.RemoveSelectedSymbol();
        }

        private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (_editorVm is null && DataContext is EditorVm editorVm)
            {
                _editorVm ??= editorVm;
            }

            _editorVm?.CreateActionSymbol();

            e.Handled = true;
        }
    }
}