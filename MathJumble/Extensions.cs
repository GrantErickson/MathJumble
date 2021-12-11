using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathJumble
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a concatenated string that is sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string StringifyOrdered<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(f => f).Stringify();
        }
        /// <summary>
        /// Joins each term of the collection with a ", "
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Stringify<T>(this IEnumerable<T> list)
        {
            return string.Join(", ", list);
        }

        /// <summary>
        /// Adds a list of T to the dictionary with a ordered string of members as the key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="list"></param>
        public static void Add<T>(this Dictionary<string, List<T>> target, List<T> list)
        {
            target.Add(list.StringifyOrdered(), list);
        }

        /// <summary>
        /// Permutes the list and adds it to the Stack.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="toAdd"></param>
        public static void PermuteAdd<T>(this Stack<CalculatedList<T>> list, CalculatedList<T> toAdd)
        {
            Permute(toAdd).ToList().ForEach(f=> list.Push(f));
        }

        /// <summary>
        /// Recursive permutation of the input CalculatedList.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<CalculatedList<T>> Permute<T>(this CalculatedList<T> source)
        {
            // This is so that we can eliminate duplicates (instead of yielding which can't find this)
            var results = new Dictionary<string, CalculatedList<T>>();
            foreach(var item in source)
            {
                // Create a new list and remove the item.
                var subset = new CalculatedList<T>(source);
                subset.Remove(item);
                // If there is only one item left then this is the end, we return this item.
                if (subset.Count() == 1)
                {
                    subset.Add(item);
                    var key = subset.Stringify();
                    // Only add to the resulting collection if this is unique.
                    if (!results.ContainsKey(key)) results.Add(key, subset);
                }
                else
                {
                    // There are more numbers to permutate. Permute what is left and then add the current number to the end.
                    foreach (var result in Permute(subset))
                    {
                        // Add this item to the end of all the permuted subset results.
                        result.Add(item);
                        var key = result.Stringify();
                        if (!results.ContainsKey(key)) results.Add(key, result);
                    }
                }
            }
            // Only return the values, the keys don't matter any longer.
            return results.Values;
        }
    }
}
