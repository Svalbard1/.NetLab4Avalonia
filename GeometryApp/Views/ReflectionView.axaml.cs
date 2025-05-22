using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GeometryApp.Views
{
    public partial class ReflectionView : UserControl
    {
        public ReflectionView()
        {
            InitializeComponent();
        }

        private void InitializeComponent() =>
            AvaloniaXamlLoader.Load(this);
    }
}
