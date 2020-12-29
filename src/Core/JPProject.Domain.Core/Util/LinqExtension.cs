using System;
using System.Collections.Generic;

namespace JPProject.Domain.Core.Util
{
    public static class LinqExtension
    {
        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
