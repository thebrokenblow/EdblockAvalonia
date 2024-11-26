using Edblock.ViewModel.Components;

namespace Edblock.ViewModel.SymbolsVM;

public class ConditionSymbolVm : SymbolVm
{
    private const int DefaultWidth = 140;
    private const int DefaultHeight = 60;

    public ConditionSymbolVm(EditorVm editorVm) : base(editorVm)
    {
        Width = DefaultWidth;
        Height = DefaultHeight;
    }
    
    public override void SetWidth(double width)
    {
        Width = width;
        _symbolModel.Width = width;
    }

    public override void SetHeight(double height)
    {
        Height = height;
        _symbolModel.Height = height;
    }
}