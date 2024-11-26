using Avalonia.Controls;
using Avalonia.Input;
using Edblock.ViewModel.Components;
using Edblock.ViewModel.SymbolsVM;

namespace Edblock.View.Views;

public partial class MainWindow : Window
{
    private EditorVm? _editorVm;
    public MainWindow()
    {
        InitializeComponent();
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