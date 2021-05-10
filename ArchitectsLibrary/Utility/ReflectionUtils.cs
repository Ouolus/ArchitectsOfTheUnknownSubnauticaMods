using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// Contains methods to help in reflection. You should use the assembly publicizer in most cases.
    /// </summary>
    public static class ReflectionUtils
    {
        public static void SetPrivateField<T>(Type type, T instance, string name, object value)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            prop.SetValue(instance, value);
        }
        public static OutputT GetPrivateField<OutputT>(Type type, object target, string name)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            return (OutputT)prop.GetValue(target);
        }
        public static void SetProtectedField<T>(Type type, T instance, string name, object value)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            prop.SetValue(instance, value);
        }
        public static OutputT GetProtectedField<OutputT>(Type type, object target, string name)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            return (OutputT)prop.GetValue(target);
        }
    }
}
