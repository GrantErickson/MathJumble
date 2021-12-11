using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathJumble
{
    /// <summary>
    /// Simple collection that also has a collection of the calculations that it takes to get an answer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CalculatedList<T> : List<T>
    {
        public CalculatedList() { }

        public CalculatedList(CalculatedList<T> list)
        {
            this.AddRange(list);
            this.Calculations.AddRange(list.Calculations);
        }

        /// <summary>
        /// List of calculations performed on this collection.
        /// </summary>
        public List<string> Calculations { get; } = new List<string>();
    }
}
