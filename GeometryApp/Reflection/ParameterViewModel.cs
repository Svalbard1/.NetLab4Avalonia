using CommunityToolkit.Mvvm.ComponentModel;

namespace GeometryApp.Reflection
{
    /// <summary>
    /// Модель для одного параметра метода: имя + значение.
    /// </summary>
    public class ParameterViewModel : ObservableObject
    {
        public string Name { get; }

        private string _value = string.Empty;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public ParameterViewModel(string name)
        {
            Name = name;
        }
    }
}