using EdblockClient;

namespace Edblock.Model.Factories;

public class FactorySymbolModel
{
    public SymbolModel Create(ResponseSymbol responseSymbol) =>
        new()
        {
            Id = responseSymbol.Id,
            X = responseSymbol.X,
            Y = responseSymbol.Y,
        };
}