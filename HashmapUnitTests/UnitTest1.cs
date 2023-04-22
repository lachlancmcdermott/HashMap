using HashMap;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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
            if(dataAmount == 17)
            {
                ;
            }
            EqualityComparer<int> equalityComparer= EqualityComparer<int>.Default;

            bool t = true;
            HashMap<int, int> map = new HashMap<int, int>();
            Random random = new Random(34);
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
    }
}