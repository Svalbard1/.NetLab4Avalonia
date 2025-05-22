using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GeometryApp.Models.Geometry;

namespace GeometryApp.Views
{
    public partial class GeometryCanvas : UserControl
    {
        private readonly Canvas _canvas;
        private readonly Random _random;
        private const double CanvasWidth = 800;
        private const double CanvasHeight = 600;

        public GeometryCanvas()
        {
            InitializeComponent();
            _canvas = this.FindControl<Canvas>("DrawingCanvas")!;
            _random = new Random();
            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            _canvas.Width = CanvasWidth;
            _canvas.Height = CanvasHeight;
            _canvas.Background = Brushes.White;
        }

        public void DrawFigures(IEnumerable<GeometryFigure> figures)
        {
            try
            {
                _canvas.Children.Clear();

                foreach (var figure in figures)
                {
                    if (figure is not GeometryApp.Models.Geometry.Polygon)
                    {
                        CreateAndAddFigure(figure, figure.CenterX, figure.CenterY);
                    }
                    else
                    {
                        CreateAndAddFigure(figure, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка в DrawFigures:");
                Console.Error.WriteLine(ex);
                throw;
            }
        }

        private void CreateAndAddFigure(GeometryFigure figure, double x, double y)
        {
            if (figure is GeometryApp.Models.Geometry.Point p)
            {
                var ellipse = new Avalonia.Controls.Shapes.Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red,
                };
                _canvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
                Canvas.SetTop(ellipse, y - ellipse.Height / 2);
            }
            else if (figure is GeometryApp.Models.Geometry.Line l)
            {
                var line = new Avalonia.Controls.Shapes.Line
                {
                    StartPoint = new Avalonia.Point(l.CenterX, l.CenterY),
                    EndPoint = new Avalonia.Point(l.X2, l.Y2),
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                _canvas.Children.Add(line);
            }
            else if (figure is GeometryApp.Models.Geometry.Ellipse e)
            {
                var ellipse = new Avalonia.Controls.Shapes.Ellipse
                {
                    Width = e.Width,
                    Height = e.Height,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2,
                };
                _canvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, x - e.Width / 2);
                Canvas.SetTop(ellipse, y - e.Height / 2);
            }
            else if (figure is GeometryApp.Models.Geometry.Polygon poly)
            {
                var polygon = new Avalonia.Controls.Shapes.Polygon
                {
                    Points = new Avalonia.Collections.AvaloniaList<Avalonia.Point>(
                        poly.Points.Select(pt => new Avalonia.Point(pt.X, pt.Y))
                    ),
                    Stroke = Brushes.Green,
                    StrokeThickness = 2,
                    Fill = Brushes.LightGreen,
                };
                _canvas.Children.Add(polygon);
            }
        }
    }
}
