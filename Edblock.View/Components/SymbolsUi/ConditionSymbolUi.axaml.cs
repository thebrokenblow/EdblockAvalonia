using Avalonia.Controls;
using Avalonia.Input;
using Edblock.View.Views;

namespace Edblock.View.Components.SymbolsUi;

public partial class ConditionSymbolUi : UserControl
{
    public MainWindow? MainWindow { get; set; }
    
    public ConditionSymbolUi()
    {
        InitializeComponent();
    }
    
    private void StartMoveSymbol(object? sender, PointerPressedEventArgs e)
    {
        MainWindow?.OnPointerPressed(sender, e);
    }
}