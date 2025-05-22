using System;

namespace GeometryApp.Models.Geometry;

public class Ellipse(double centerX, double centerY, double width, double height)
    : GeometryFigure(centerX, centerY)
{
    public double Width { get; } = width;
    public double Height { get; } = height;

    public override double GetArea() => Math.PI * (Width / 2) * (Height / 2);

    public override (double X, double Y, double Width, double Height) GetBoundingBox()
    {
        return (CenterX - Width / 2, CenterY - Height / 2, Width, Height);
    }
}
