using System;

namespace GeometryApp.Models.Geometry;

public class Line(double x1, double y1, double x2, double y2) : GeometryFigure(x1, y1)
{
    public double X2 { get; } = x2;
    public double Y2 { get; } = y2;

    public override double GetArea() => 0;

    public override (double X, double Y, double Width, double Height) GetBoundingBox()
    {
        double minX = Math.Min(CenterX, X2);
        double minY = Math.Min(CenterY, Y2);
        double width = Math.Abs(X2 - CenterX);
        double height = Math.Abs(Y2 - CenterY);

        return (minX, minY, width, height);
    }
}
