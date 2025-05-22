using System;
using Avalonia.Controls;
using GeometryApp.ViewModels;

namespace GeometryApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly GeometryCanvas _canvasView;
        private GeometryViewModel? _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow инициализирован");

            _canvasView =
                this.FindControl<GeometryCanvas>("CanvasView")
                ?? throw new NullReferenceException("CanvasView не найден");

            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            _viewModel = DataContext as GeometryViewModel;
            if (_viewModel != null)
            {
                _viewModel.Figures.CollectionChanged += (s, e) =>
                {
                    _canvasView.DrawFigures(_viewModel.Figures);
                };
                _canvasView.DrawFigures(_viewModel.Figures);
            }
        }
    }
}
