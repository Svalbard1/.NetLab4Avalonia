using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeometryApp.Models.Geometry;

namespace GeometryApp.ViewModels
{
    public class GeometryViewModel : ObservableObject
    {
        public ObservableCollection<GeometryFigure> Figures { get; } = new();

        private readonly Random _random = new Random();

        private const double CanvasWidth = 800;
        private const double CanvasHeight = 600;

        public ICommand AddPointCommand { get; }
        public ICommand AddLineCommand { get; }
        public ICommand AddEllipseCommand { get; }
        public ICommand AddPolygonCommand { get; }

        public GeometryViewModel()
        {
            AddPointCommand = new RelayCommand(() =>
            {
                double x = _random.NextDouble() * (CanvasWidth - 10) + 5;
                double y = _random.NextDouble() * (CanvasHeight - 10) + 5;
                Figures.Add(new Point(x, y));
            });

            AddLineCommand = new RelayCommand(() =>
            {
                double x1 = _random.NextDouble() * (CanvasWidth - 10) + 5;
                double y1 = _random.NextDouble() * (CanvasHeight - 10) + 5;
                double x2 = _random.NextDouble() * (CanvasWidth - 10) + 5;
                double y2 = _random.NextDouble() * (CanvasHeight - 10) + 5;
                Figures.Add(new Line(x1, y1, x2, y2));
            });

            AddEllipseCommand = new RelayCommand(() =>
            {
                double x = _random.NextDouble() * (CanvasWidth - 80) + 40;
                double y = _random.NextDouble() * (CanvasHeight - 50) + 25;
                double width = 80;
                double height = 50;
                Figures.Add(new Ellipse(x, y, width, height));
            });

            AddPolygonCommand = new RelayCommand(() =>
            {
                var relativePoints = new List<(double X, double Y)> { (0, 0), (50, 0), (25, 50) };
                double maxX = 800 - 50;
                double maxY = 600 - 50;
                double x = _random.NextDouble() * maxX;
                double y = _random.NextDouble() * maxY;

                var shiftedPoints = relativePoints.Select(pt => (pt.X + x, pt.Y + y)).ToList();
                Figures.Add(new Polygon(shiftedPoints));
            });
        }
    }
}
