using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathJumble
{
    public class CalculatedList<T> : List<T>
    {
        public CalculatedList() { }
        public CalculatedList(List<T> list){
            this.AddRange(list);
        }
        public CalculatedList(CalculatedList<T> list)
        {
            this.AddRange(list);
            this.Calculations.AddRange(list.Calculations);
        }

        public List<string> Calculations { get; } = new List<string>();
    }
}
