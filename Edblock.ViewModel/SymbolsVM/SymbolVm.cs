using System.ComponentModel;
using System.Runtime.CompilerServices;
using Edblock.Model;
using Edblock.ViewModel.Components;

namespace Edblock.ViewModel.SymbolsVM;

public abstract class SymbolVm : INotifyPropertyChanged
{
    public string Id { get; set; }
    public string Name { get; set; }
    private double _x;

    public double X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged();
        }
    }

    private double y;
    public double Y
    {
        get => y;
        set
        {
            y = value;
            OnPropertyChanged();
        }
    }
    public double Width { get; set; }
    public double Height { get; set; }
    public string Background { get; set; }
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
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}