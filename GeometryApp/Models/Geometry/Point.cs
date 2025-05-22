namespace GeometryApp.Models.Geometry;

public class Point(double x, double y) : GeometryFigure(x, y)
{
    public override double GetArea() => 0;

    public override (double X, double Y, double Width, double Height) GetBoundingBox() =>
        (CenterX, CenterY, 0, 0);
}
