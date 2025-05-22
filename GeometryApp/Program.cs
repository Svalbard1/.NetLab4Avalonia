using System;
using Avalonia;

namespace GeometryApp
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка при запуске приложения:");
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
    }
}
