using GeometryApp.Plugins.Contracts;

namespace GeometryApp.Plugins
{
    public class Greeter : ICallable
    {
        public string DisplayName => "Приветствие";

        public string SayHello(string name) => $"Hello, {name}!";
    }
}
