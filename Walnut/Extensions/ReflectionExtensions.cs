using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Walnut.Extensions
{
    public static class ReflectionExtensions //must be static to contain extension methods
    {
        public static string GetPropertyValue<T>(this T _someEntity, string PropertyName)
        {
            return _someEntity.GetType().GetProperty(PropertyName).GetValue(_someEntity, null).ToString();
        }
    }
}