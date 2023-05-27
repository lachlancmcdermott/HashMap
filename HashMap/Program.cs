using System;
using System.Collections.Generic;
using System.Linq;

namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<int, int> map = new HashMap<int, int>();
            map.Add(21, 32);
            map.Add(-2, 42);
            map.Add(0, 62);
            map.Add(12, 1232);
            map.Add(9, 532);


            foreach (var n in map)
            {
                Console.WriteLine(n);
            }
        }
    }
}