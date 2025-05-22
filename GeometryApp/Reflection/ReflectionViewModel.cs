using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GeometryApp.Reflection
{
    public partial class ReflectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string assemblyPath = string.Empty;

        [ObservableProperty]
        private ObservableCollection<LoadedClassInfo> classes = new();

        [ObservableProperty]
        private LoadedClassInfo? selectedClass;

        [ObservableProperty]
        private MethodInfo? selectedMethod;

        /// <summary>
        /// Новая коллекция параметров с именами и значениями.
        /// </summary>
        public ObservableCollection<ParameterViewModel> Parameters { get; }
            = new ObservableCollection<ParameterViewModel>();

        [ObservableProperty]
        private string result = string.Empty;

        public IRelayCommand LoadAssemblyCommand { get; }
        public IRelayCommand ExecuteMethodCommand { get; }

        public ReflectionViewModel()
        {
            LoadAssemblyCommand  = new RelayCommand(LoadAssembly);
            ExecuteMethodCommand = new RelayCommand(ExecuteMethod, () => SelectedMethod != null);
        }

        private void LoadAssembly()
        {
            if (!File.Exists(AssemblyPath))
                return;

            try
            {
                var asm = Assembly.LoadFrom(AssemblyPath);
                var iface = asm.GetTypes()
                               .FirstOrDefault(t => t.IsInterface && t.Name == "ICallable");
                if (iface == null)
                    return;

                Classes.Clear();
                foreach (var type in asm.GetTypes()
                                        .Where(t => iface.IsAssignableFrom(t)
                                                    && !t.IsInterface
                                                    && !t.IsAbstract))
                {
                    Classes.Add(new LoadedClassInfo
                    {
                        Type    = type,
                        Methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                      .Where(m => !m.IsSpecialName)
                                      .ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                Result = $"Ошибка загрузки сборки: {ex.Message}";
            }
        }

        partial void OnSelectedClassChanged(LoadedClassInfo? value)
        {
            SelectedMethod = null;
            Parameters.Clear();
            Result = string.Empty;
            ExecuteMethodCommand.NotifyCanExecuteChanged();
        }

        partial void OnSelectedMethodChanged(MethodInfo? value)
        {
            Parameters.Clear();
            if (value != null)
            {
                foreach (var p in value.GetParameters())
                    Parameters.Add(new ParameterViewModel(p.Name));
            }
            Result = string.Empty;
            ExecuteMethodCommand.NotifyCanExecuteChanged();
        }

        private void ExecuteMethod()
        {
            if (SelectedClass == null || SelectedMethod == null)
                return;

            try
            {
                var instance = Activator.CreateInstance(SelectedClass.Type)!;
                var args = SelectedMethod.GetParameters()
                    .Select((p, i) => Convert.ChangeType(Parameters[i].Value, p.ParameterType))
                    .ToArray();

                var ret = SelectedMethod.Invoke(instance, args);
                Result = ret?.ToString() ?? "<null>";
            }
            catch (Exception ex)
            {
                Result = $"Ошибка выполнения метода: {ex.Message}";
            }
        }
    }
}