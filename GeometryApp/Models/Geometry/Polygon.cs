using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryApp.Models.Geometry;

public class Polygon(IEnumerable<(double X, double Y)> points)
    : GeometryFigure(points.First().X, points.First().Y)
{
    public List<(double X, double Y)> Points { get; } = [.. points];

    public override double GetArea()
    {
        double area = 0;
        int n = Points.Count;
        for (int i = 0; i < n; i++)
        {
            var (x1, y1) = Points[i];
            var (x2, y2) = Points[(i + 1) % n];
            area += (x1 * y2 - x2 * y1);
        }
        return Math.Abs(area) / 2.0;
    }

    public override (double X, double Y, double Width, double Height) GetBoundingBox()
    {
        double minX = Points.Min(p => p.X);
        double maxX = Points.Max(p => p.X);
        double minY = Points.Min(p => p.Y);
        double maxY = Points.Max(p => p.Y);
        return (minX, minY, maxX - minX, maxY - minY);
    }
}
