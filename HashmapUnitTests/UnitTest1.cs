using HashMap;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace HashmapUnitTests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        [DataRow(3)]
        [DataRow(17)]
        [DataRow(1)]
        [DataRow(3000)]
        [DataRow(42)]
        [DataRow(30)]

        public void TestInsertion(int dataAmount)
        {
            EqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;
            bool t = true;
            HashMap<int, int> map = new HashMap<int, int>();
            Random random = new Random(23451);
            for (int i = 0; i < dataAmount; i++)
            {
                int val = random.Next();
                int key = random.Next();
                map.Add(key, val);
                int hash = Math.Abs(equalityComparer.GetHashCode(key)) % map.buckets.Length;
                Assert.IsNotNull(map.buckets[hash]);
                if (!map.buckets[hash].Contains((key, val))) t = false;
            }
            Assert.IsTrue(t);
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(17)]
        [DataRow(1)]
        [DataRow(3000)]
        [DataRow(42)]
        [DataRow(30)]

        public void TestRemove(int dataAmount)
        {
            EqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;
            bool t = true;
            HashMap<int, int> map = new HashMap<int, int>();
            Random randomVal = new Random(23451);
            Random randomKey = new Random(2);
            for (int i = 0; i < dataAmount; i++)
            {
                int val = randomVal.Next();
                int key = randomKey.Next();
                map.Add(key, val);
            }
            Random randomValRegenerate = new Random(23451);
            Random randomKeyRegenerate = new Random(2);
            for (int i = 0; i < dataAmount; i++)
            {
                int u = randomValRegenerate.Next();
                int m = randomKeyRegenerate.Next();
                map.Remove(u);
                Assert.IsFalse(map.Contains(new KeyValuePair<int, int>(u, m)));
            }
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(17)]
        [DataRow(1)]
        [DataRow(3000)]
        [DataRow(42)]
        [DataRow(30)]
        public void TestIndexing(int dataAmount)
        {
            EqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;
            bool t = true;
            HashMap<int, int> map = new HashMap<int, int>();
            Random randomVal = new Random(23451);
            Random randomKey = new Random(2);
            for (int i = 0; i < dataAmount; i++)
            {
                int val = randomVal.Next();
                int key = randomKey.Next();
                map.Add(key, val);
            }
            Random randomValRegenerate = new Random(23451);
            Random randomKeyRegenerate = new Random(2);
            for (int i = 0; i < dataAmount; i++)
            {
                int u = randomValRegenerate.Next();
                int m = randomKeyRegenerate.Next();

                (int, int) q = (m, map[i]);
                (int, int) z = (m, u);
                Assert.AreEqual(q, z);
            }
        }
        //[TestMethod]
        //[DataRow(3)]
        //[DataRow(17)]
        //[DataRow(1)]
        //[DataRow(3000)]
        //[DataRow(42)]
        //[DataRow(30)]
        //public void TestEnumeration(int dataAmount)
        //{

        //}
    }
}