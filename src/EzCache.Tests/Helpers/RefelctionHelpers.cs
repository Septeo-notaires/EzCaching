using System.Reflection;
using System.Runtime.CompilerServices;

namespace EzCache.Tests.Helpers
{
    internal static class RefelctionHelpers
    {
        private const BindingFlags privateFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public static TValue? GetPropertyValue<T, TValue>(this T instance, string propertyName)
            where T : class
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(T), propertyName);
            return (TValue?)propertyInfo?.GetValue(instance);
        }

        public static void SetPropertyValue<T, TValue>(this T instance, string propertyName, TValue value)
            where T : class
        {
            PropertyInfo propertyInfo =  GetPropertyInfo(typeof(T), propertyName);
            propertyInfo.SetValue(instance, value);
        }
        
        public static TValue? GetFieldValue<T, TValue>(this T instance, string fieldName)
            where T : class
        {
            FieldInfo fieldInfo = GetFieldInfo(typeof(T), fieldName);
            return (TValue?)fieldInfo?.GetValue(instance);
        }

        public static void SetFieldValue<T, TValue>(this T instance, string fieldName, TValue value)
        where T : class
        {
            FieldInfo fieldInfo =  GetFieldInfo(typeof(T), fieldName);
            fieldInfo.SetValue(instance, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo? propertyInfo = type.GetProperty(propertyName, privateFlags);
            if (propertyInfo == null)
                throw new ArgumentException($"Property {propertyName} not found in type {type}");

            return propertyInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo? fieldInfo = type.GetField(fieldName, privateFlags);
            if (fieldInfo == null)
                throw new ArgumentException($"Field {fieldName} not found in type {type}");

            return fieldInfo;
        }
    }
}
