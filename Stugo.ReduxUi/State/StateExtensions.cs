using System;
using System.Linq;

namespace Stugo.ReduxUi.State
{
    public static class StateExtensions
    {
        /// <summary>
        /// Create a new array with the given element appended to the given array.
        /// </summary>
        public static T[] Append<T>(this T[] array, T item)
        {
            var newArray = new T[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[array.Length] = item;
            return newArray;
        }


        /// <summary>
        /// Create a new array with the given update function applied to each element in the given
        /// array.
        /// </summary>
        public static T[] Update<T>(this T[] array, Func<T, T> update)
        {
            return array.Select(update).ToArray();
        }


        /// <summary>
        /// Create a new array with the given element removed from the given array.
        /// </summary>
        public static T[] Remove<T>(this T[] array, T item)
        {
            return array.Where(x => !x.Equals(item)).ToArray();
        }
    }
}
