using System;
using System.Collections.Generic;
using System.Linq;

namespace MathJumble
{
    class Program
    {
        // Dictionary to hold everything we have tried unordered
        private static readonly Dictionary<string, List<decimal>> _all = new Dictionary<string, List<decimal>>();
        // List of available math functions
        private static readonly Dictionary<string, Func<decimal, decimal, decimal>> _functions = new Dictionary<string, Func<decimal, decimal, decimal>>
        {
            { "+", (arg1, arg2) => arg1 + arg2 },
            { "-", (arg1, arg2) => arg1 - arg2 },
            { "*", (arg1, arg2) => arg1 * arg2 },
            { "/", (arg1, arg2) => arg1 / arg2 },
        };

        static void Main(string[] args)
        {
            decimal target = 576;
            var startingNumbers = new CalculatedList<decimal> { 4, 7, 10, 15, 25, 50 };
            var allowFractions = false;
            var allowNegatives = false;
            //decimal target = 5;
            //var startingNumbers = new CalculatedList<decimal> { 1, 2, 3 };
            var wins = new List<CalculatedList<decimal>>();

            _all.Add(startingNumbers);

            var remaining = new Stack<CalculatedList<decimal>>();
            remaining.PermuteAdd(startingNumbers);

            do
            {
                var currentList = remaining.Pop();
                //Console.WriteLine($"{currentList.Stringify()}: {currentList.Calculations.Stringify()}");
                foreach (var fx in _functions)
                {
                    var list = new CalculatedList<decimal>(currentList);
                    try
                    {
                        var result = fx.Value(list[0], list[1]);
                        if ((result == (int)result || allowFractions) && (result >= 0 || allowNegatives)) {
                            if (result.ToString().Length < 6)
                            {
                                list.Calculations.Add($"{list[0]}{fx.Key}{list[1]}={result}");
                                list.RemoveAt(1);
                                list.RemoveAt(0);
                                list.Add(result);
                                if (result == target)
                                {
                                    wins.Add(list);
                                }
                                if (list.Count > 1)
                                {
                                    if (!_all.ContainsKey(list.StringifyOrdered()))
                                    {
                                        _all.Add(list.StringifyOrdered(), list);
                                        remaining.PermuteAdd(list);
                                    }
                                }
                            }
                        }
                    }
                    catch (DivideByZeroException) { } // Swallow this. 
                    catch (OverflowException) { } // Swallow this.

                }
            } while (remaining.Count > 0);

            // Write out the results.
            var min = wins.Min(f => f.Calculations.Count);
            Console.WriteLine($"Possible in {min}");

            foreach (var win in wins.Where(f => f.Calculations.Count == min)) 
            {
                Console.WriteLine(win.Calculations.Stringify());
            }
        }
    }
}
