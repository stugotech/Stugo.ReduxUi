using System;
using System.Collections.Generic;

namespace Stugo.ReduxUi
{
    public static class TypeAnalayser
    {
        public static Type[] GetAllTypes(Type type)
        {
            var types = new List<Type> { type };
            types.AddRange(type.GetInterfaces());

            for (var t = type.BaseType; t != null; t = t.BaseType)
            {
                types.Add(t);   
            }

            return types.ToArray();
        }
    }
}
