using System;

namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, int> hashMap = new HashMap<string, int>();
            hashMap.Add("cat", 1);
            hashMap.Add("dog", 2);
            hashMap.Add("frog", 3);
        }
    }
}