using System;
using System.Linq;

namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, int> hashMap = new HashMap<string, int>();

            hashMap.Add("a", 1);
            hashMap.Add("b", 2);    
            hashMap.Add("c", 3);
            hashMap.Add("d", 4);
            hashMap.Add("e", 5);

            bool contains = hashMap.Contains(new KeyValuePair<string, int>("a", 1));

            hashMap.Remove("a");
            hashMap.Remove("b");
            hashMap.Remove("c");

            bool indexing = hashMap.Indexing("d");
        }
    }
}