using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Edblock.View.Components;

namespace Edblock.View.Core.Configurations;

public class ConfigurationDraggableSymbolUi
{
    private readonly MenuDraggableSymbolUi _menuDraggableSymbolUi;
    
    public ConfigurationDraggableSymbolUi(MenuDraggableSymbolUi menuDraggableSymbolUi)
    {
        _menuDraggableSymbolUi = menuDraggableSymbolUi;
    }

    public Dictionary<string, Control> Сonfigure() =>
        new()
        {
            { _menuDraggableSymbolUi.DraggableActionSymbolUi.ActionSymbol.Name!, _menuDraggableSymbolUi.DraggableActionSymbolUi.ActionSymbol },
            { _menuDraggableSymbolUi.DraggableConditionSymbolUi.ConditionSymbol.Name!, _menuDraggableSymbolUi.DraggableConditionSymbolUi.ConditionSymbol }
        };
}