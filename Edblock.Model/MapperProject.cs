using Edblock.Model.Factories;
using EdblockClient;

namespace Edblock.Model;

public class MapperProject
{
    private readonly FactorySymbolModel _factorySymbolModel;
    
    public MapperProject(FactorySymbolModel factorySymbolModel)
    {
        _factorySymbolModel = factorySymbolModel;
    }

    public List<SymbolModel> MapProject(List<ResponseSymbol> responseSymbols) =>
        responseSymbols.Select(
            responseSymbol => _factorySymbolModel
                                .Create(responseSymbol))
            .ToList();
}