using Avalonia;
using Edblock.ViewModel.Components;

namespace Edblock.ViewModel.SymbolsVM;

public class ConditionSymbolVm : SymbolVm
{
    private const int DefaultWidth = 140;
    private const int DefaultHeight = 60;
    private List<Point> Points { get; set; } = [];
    
    public ConditionSymbolVm(EditorVm editorVm) : base(editorVm)
    {
        Width = DefaultWidth;
        Height = DefaultHeight;
        Background = "#FF60B2D3";
        
        SetCoordinatePolygonPoints();
    }
    
    public override void SetWidth(double width)
    {     
        Width = width;
        SetCoordinatePolygonPoints();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        SetCoordinatePolygonPoints();
    }

    private void SetCoordinatePolygonPoints()
    {
        var halfWidth = Width / 2;
        var halfHeight = Height / 2;

        Points =
        [
            new Point(halfWidth, Height),
            new Point(Width, halfHeight),
            new Point(halfWidth, 0),
            new Point(0, halfHeight)
        ];
    }
}