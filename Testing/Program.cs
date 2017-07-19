using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Pair<int> pair = new Pair<int>(1,1);

            Console.WriteLine(pair.item1 + pair.item2);

            Console.ReadKey();
        }
    }
}
