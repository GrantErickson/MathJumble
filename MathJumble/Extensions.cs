using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathJumble
{
    public static class Extensions
    {
        public static string StringifyOrdered<T>(this List<T> list)
        {
            return string.Join(", ", list.OrderBy(f => f));
        }
        public static string Stringify<T>(this List<T> list)
        {
            return string.Join(", ", list);
        }

        public static void Add<T>(this Dictionary<string, List<T>> target, List<T> list)
        {
            target.Add(list.StringifyOrdered(), list);
        }

        public static void PermuteAdd<T>(this Stack<CalculatedList<T>> list, CalculatedList<T> toAdd)
        {
            Permute(toAdd).ToList().ForEach(f=> list.Push(f));
        }

        public static IEnumerable<CalculatedList<T>> Permute<T>(this CalculatedList<T> source)
        {
            // This is so that we can eliminate duplicates (instead of yielding which can't find this)
            var results = new Dictionary<string, CalculatedList<T>>();
            foreach(var item in source)
            {
                var subset = new CalculatedList<T>(source);
                subset.Remove(item);
                if (subset.Count() == 1)
                {
                    subset.Add(item);
                    var key = subset.Stringify();
                    if (!results.ContainsKey(key)) results.Add(key, subset);
                }
                else
                {
                    foreach (var result in Permute(subset))
                    {
                        result.Add(item);
                        var key = result.Stringify();
                        if (!results.ContainsKey(key)) results.Add(key, result);
                    }
                }
            }
            return results.Values;
        }
    }
}
