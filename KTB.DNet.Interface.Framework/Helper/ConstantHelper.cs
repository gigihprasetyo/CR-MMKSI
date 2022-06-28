using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Framework
{
    public static class ConstantHelper
    {
        public static List<T> GetListOfConstantValue<T>(Type type)
        {
            try
            {
                return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Where(f => f.IsLiteral && !f.IsInitOnly).Select(prop =>
                        {
                            T v = (T)prop.GetValue(null);
                            return v;
                        }).ToList<T>();
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
    }
}
