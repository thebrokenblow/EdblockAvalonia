using Edblock.Model;
using Edblock.ViewModel.Factories;
using Edblock.ViewModel.SymbolsVM;

namespace Edblock.ViewModel.Core;

public class Mapper(FactorySymbolVm factorySymbolVm)
{
    public List<SymbolVm> Map(List<SymbolModel> symbols) =>
        symbols.Select(responseSymbol => factorySymbolVm.Create(responseSymbol.Name)).ToList();
}