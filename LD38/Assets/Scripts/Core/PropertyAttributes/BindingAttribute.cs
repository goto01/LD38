using UnityEngine;

namespace Assets.Scripts.Core.PropertyAttributes
{
    public class BindingAttribute : PropertyAttribute
    {
        public bool IsRequired { get; private set; }

        public BindingAttribute(bool isRequired)
        {
            IsRequired = isRequired;
        }
    }
}
