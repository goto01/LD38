using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Core.Staff.Helpers
{
    static class SystemTypeExtended
    {
        public static IList<FieldInfo> GetFieldsByAttributeType<T>(this Type type) where T: Attribute
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.GetCustomAttributes(true).Any(p => p is T)).ToList();
        }

        public static IList<FieldInfo> GetAllFields(this Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static FieldInfo GetNonPublicField(this Type type, string fieldName)
        {
            return type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static IList<PropertyInfo> GetAllProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static PropertyInfo GetNonPublicProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
