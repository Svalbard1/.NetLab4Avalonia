using System;
using Avalonia;

namespace GeometryApp.Models;

public abstract class Shape
{
    public Point Center { get; set; }

    // Добавим проверку вычисления BoundingBox
    public virtual void LogBounds()
    {
        Console.WriteLine($"Bounds: {BoundingBox}");
    }

    public abstract Rect BoundingBox { get; }
    public abstract double Area { get; }
}
