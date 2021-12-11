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
            // Constraings
            decimal target = 36;
            var startingNumbers = new CalculatedList<decimal> { 1, 2, 4, 5 };

            // Original puzzle numbers
            //decimal target = 576;
            //var startingNumbers = new CalculatedList<decimal> { 4, 7, 10, 15, 25, 50 };


            // Options
            var allowFractions = false;
            var allowNegatives = false;
            var maxIntermediateNumberLength = 6;

            // Keep track of the things that work.
            var wins = new List<CalculatedList<decimal>>();

            // Stack that will keep all the things that need to tried.
            var remaining = new Stack<CalculatedList<decimal>>();
            remaining.PermuteAdd(startingNumbers);
            _all.Add(startingNumbers.StringifyOrdered(), startingNumbers);

            do
            {
                // Take the first item off the stack
                var currentList = remaining.Pop();
                // Try each operation on the first two numbers.
                foreach (var fx in _functions)
                {
                    // Create a new list to contain the results.
                    try
                    {
                        // Calculate on the first two numbers in the list.
                        var result = fx.Value(currentList[0], currentList[1]);
                        // Check if there is any reason to throw away the result based on constraints.
                        if ((result == (int)result || allowFractions) &&
                            (result >= 0 || allowNegatives) &&
                            (result.ToString().Length <= maxIntermediateNumberLength))
                        {
                            // Create a new list
                            var list = new CalculatedList<decimal>(currentList);
                            // Add the calculation to the list of calculations so we can know the steps.
                            list.Calculations.Add($"{list[0]}{fx.Key}{list[1]}={result}");
                            // Remove the two items we just used in the calculation
                            list.RemoveAt(1);
                            list.RemoveAt(0);
                            // Add the newly calculated number at the end.
                            list.Add(result);
                            // Did we find the target solution? Ff so add to the list of wins.
                            if (result == target)
                            {
                                wins.Add(list);
                            }
                            // If there are still two things in the list try again.
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
                    catch (DivideByZeroException) { } // Swallow this. 
                    catch (OverflowException) { } // Swallow this.
                }
            } while (remaining.Count > 0);

            // Find the smallest number of calculations
            var min = wins.Min(f => f.Calculations.Count);
            Console.WriteLine($"Possible in {min}");

            // Write out the results.
            foreach (var win in wins.Where(f => f.Calculations.Count == min))
            {
                Console.WriteLine(win.Calculations.Stringify());
            }
        }
    }
}
