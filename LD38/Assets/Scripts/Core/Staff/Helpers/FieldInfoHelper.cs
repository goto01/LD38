using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core.PropertyAttributes;

namespace Assets.Scripts.Core.Staff.Helpers
{
    public static class FieldInfoExtended
    {
        public static T GetAttribute<T>(this FieldInfo fieldInfo) where T : Attribute
        {
            return fieldInfo.GetCustomAttributes(true).FirstOrDefault(x => x is BindingAttribute) as T;
        }
    }
}
