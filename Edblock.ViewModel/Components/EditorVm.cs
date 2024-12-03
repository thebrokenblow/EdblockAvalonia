using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Edblock.Model;
using Edblock.Model.Factories;
using Edblock.ViewModel.Core;
using Edblock.ViewModel.SymbolsVM;

namespace Edblock.ViewModel.Components;

public partial class EditorVm : ObservableObject
{
    public SymbolVm? SelectedSymbol { get; set; }
    private readonly ProjectService _projectService = new();
    
    [ObservableProperty]
    private ObservableCollection<SymbolVm>? _symbols = [];

    private readonly Mapper _mapper;
    public EditorVm(Mapper mapper)
    {
        _mapper = mapper;

        _symbols.Add(new ActionSymbolVm(this)
        {
            X = 100,
            Y = 200,
        });
        
        _symbols.Add(new ActionSymbolVm(this)
        {
            X = 200,
            Y = 400,
        });
        
        _symbols.Add(new ConditionSymbolVm(this)
        {
            X = 100,
            Y = 100,
        });
    }

    private async Task GetSymbolsAsync()
    {
        var responseSymbols = await _projectService.GetAsync();
        var mapperProject = new MapperProject(new FactorySymbolModel());
        var symbolModels = mapperProject.MapProject(responseSymbols);
        Symbols = new ObservableCollection<SymbolVm>(_mapper.Map(symbolModels));
    }
    
    public void ChangeCoordinateSelectedSymbol(double x, double y)
    {
        SelectedSymbol?.ChangeCoordinate(x, y);
    }

    public void RemoveSelectedSymbol()
    {
        SelectedSymbol = null;
    }

    public void CreateActionSymbol()
    {
        var symbol = new ActionSymbolVm(this);

        SelectedSymbol = symbol;
        Symbols?.Add(symbol);
    }
}