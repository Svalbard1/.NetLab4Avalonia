namespace GeometryApp.Models.Geometry;

public abstract class GeometryFigure(double centerX, double centerY)
{
    public double CenterX { get; set; } = centerX;
    public double CenterY { get; set; } = centerY;

    public abstract double GetArea();
    public abstract (double X, double Y, double Width, double Height) GetBoundingBox();
}
