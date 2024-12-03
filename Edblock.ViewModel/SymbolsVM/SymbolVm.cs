using CommunityToolkit.Mvvm.ComponentModel;
using Edblock.Model;
using Edblock.ViewModel.Components;

namespace Edblock.ViewModel.SymbolsVM;

public abstract partial class SymbolVm : ObservableObject
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    [ObservableProperty] private double _x;
    
    [ObservableProperty] private double _y;

    [ObservableProperty] private double _width;
        
    [ObservableProperty] private double _height;

    [ObservableProperty] private string _background;
    
    protected readonly SymbolModel _symbolModel;
    protected readonly EditorVm _editorVm;

    public SymbolVm(EditorVm editorVm)
    {
        _editorVm = editorVm;
        
        Id = Guid.NewGuid().ToString();
        Name = GetType().ToString();
        
        _symbolModel = new SymbolModel()
        {
            Id = Id,
            Name = Name,
        };
    }
    
    public abstract void SetWidth(double width);
    public abstract void SetHeight(double height);

    public void SetSelectedProperties()
    {
        _editorVm.SelectedSymbol = this;
    }

    public void ChangeCoordinate(double x, double y)
    {
        X = x;
        Y = y;

        _symbolModel.X = x;
        _symbolModel.Y = y;
    }
}