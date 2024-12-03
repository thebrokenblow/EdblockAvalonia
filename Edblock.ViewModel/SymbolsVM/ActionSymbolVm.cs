using Edblock.ViewModel.Components;

namespace Edblock.ViewModel.SymbolsVM;

public class ActionSymbolVm : SymbolVm
{
    private const int DefaultWidth = 140;
    private const int DefaultHeight = 60;

    public ActionSymbolVm(EditorVm editorVm) : base(editorVm)
    {
        Background = "#FF52C0AA";
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