using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeometryApp.Reflection
{
    public class LoadedClassInfo
    {
        public Type Type { get; set; } = null!;
        public string Name => Type.Name;
        public List<MethodInfo> Methods { get; set; } = new();
    }
}
