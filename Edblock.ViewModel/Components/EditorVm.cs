using System.Collections.ObjectModel;
using Edblock.ViewModel.SymbolsVM;

namespace Edblock.ViewModel.Components;

public class EditorVm
{
    public SymbolVm? SelectedSymbol { get; set; }
    public ObservableCollection<SymbolVm> Symbols { get; set; } = new();
    public EditorVm()
    {
        var symbol1 = new ActionSymbolVm(this)
        {
            X = 100,
            Y = 100
        };

        var symbol2 = new ActionSymbolVm(this)
        {
            X = 500,
            Y = 500
        };
        
        Symbols.Add(symbol1);
        Symbols.Add(symbol2);
    }

    public void ChangeCoordinateSelectedSymbol(double x, double y)
    {
        if (SelectedSymbol is null) return;
        
        SelectedSymbol.X = x;
        SelectedSymbol.Y = y;
    }

    public void RemoveSelectedSymbol()
    {
        SelectedSymbol = null;
    }

    public void CreateActionSymbol()
    {
        var symbol = new ActionSymbolVm(this)
        {
            X = 0,
            Y = 0
        };

        SelectedSymbol = symbol;
        Symbols.Add(symbol);
    }
}