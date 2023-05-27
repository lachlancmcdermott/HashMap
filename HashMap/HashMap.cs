using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HashMap
{
        public class HashMap<TKey, TValue> : IEnumerable<TKey>
    {
        public LinkedList<(TKey, TValue)>[] buckets = new LinkedList<(TKey, TValue)>[3];
        public TValue this[TKey key]
        {
            get
            {
                int hash = Math.Abs(equalityComparer.GetHashCode(key!)) % buckets.Length;
                if (buckets[hash] != null)
                {
                    LinkedListNode<(TKey, TValue)> curr = buckets[hash].First!;
                    for (int i = 0; i < buckets[hash].Count; i++)
                    {
                        if (curr!.Value.Item1!.Equals(key))
                        {
                            return curr.Value.Item2; 
                        }
                        curr = curr.Next!;
                    }
                }
                throw new ArgumentException("Key doesn't exist in hash map");
            }
            set
            {
#nullable enable
                bool exists = false;
                int hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
                LinkedListNode<(TKey, TValue)> curr = buckets[hash].First;

                for (int i = 0; i < buckets[hash].Count; i++)
                {
#nullable enable
                    if (curr.Value.Item1.Equals(key))
                    {
                        curr.ValueRef.Item2 = value;
                        exists = true;
                    }
                    curr = curr.Next;
                }
                if (!exists) Add(key, value);
            }
        }

        private int keyCount = 0;

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> newKeys = new List<TKey>();
                foreach (var n in buckets)
                {
                    if (n != null)
                    {
                        foreach (var kvp in n)
                        {
                            newKeys.Add(kvp.Item1);
                        }
                    }
                }

                return newKeys;
            }
        }
        IEqualityComparer<TKey> equalityComparer => EqualityComparer<TKey>.Default;
        public void Add(TKey key, TValue value)
        {
            int hash;
            keyCount++;

            //REHASH
            if (keyCount > buckets.Length)
            {
             
                //LinkedListNode<(TKey, TValue)> curr = new LinkedListNode<(TKey, TValue)>((default, default));
                LinkedList<(TKey, TValue)>[] newList = new LinkedList<(TKey, TValue)>[buckets.Length * 2];
                for (int i = 0; i < buckets.Length; i++)
                {
                    if (buckets[i] != null)
                    {
                        var curr = buckets[i].First;
                        while(buckets[i].Count >= 0)
                        {
                            int tempHash = Math.Abs(equalityComparer.GetHashCode(curr.Value.Item1) % newList.Length);

                            if (newList[tempHash] == null)
                            {
                                LinkedList<(TKey, TValue)> t = new LinkedList<(TKey, TValue)>();
                                newList[tempHash] = t;
                            }
                            newList[tempHash].AddLast(new LinkedListNode<(TKey, TValue)>((curr.Value.Item1, curr.Value.Item2)));
                            buckets[i].RemoveFirst();
                            if (buckets[i].First == null)
                            {
                                break;
                            }
                            curr = buckets[i].First;
                        }
                    }
                }
                buckets = newList;
            }

            //ADDING
            hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
            if (buckets[hash] != null && buckets[hash].First != null)
            {

                LinkedListNode<(TKey, TValue)> temp = buckets[hash].Find((key, value));
                if (temp == null)
                {
                    buckets[hash].AddLast(new LinkedListNode<(TKey, TValue)>((key, value)));
                }
                else
                {
                    Exception bad = new Exception("Key/Value combo already exists");
                    throw bad;
                }
            }
            if (buckets[hash] == null)
            {
                hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
                buckets[hash] = new LinkedList<(TKey, TValue)>();
                buckets[hash].AddLast(new LinkedListNode<(TKey, TValue)>((key, value)));
            }
        }
        public bool Contains(KeyValuePair<TKey, TValue> Item)
        {
            bool t = false;
            int hash = Math.Abs(equalityComparer.GetHashCode(Item.Key)) % buckets.Length;
            if (buckets[hash] != null)
            {
                foreach (var item in buckets[hash])
                {
                   if(item.Item1.Equals(Item.Key) && item.Item2.Equals(Item.Value))
                   {
                        t = true;
                   }
                }
            }
            return t;
        }
        public bool Remove(TKey key)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                {
                    foreach (var item in buckets[i])
                    {
                        if (item.Item1.Equals(key))
                        {
                            buckets[i].Remove(item);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return new Enumerator(this.Keys);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<TKey>
        {
            TKey curr;
            IEnumerator<TKey> myEnum;
            ICollection<TKey> myKeys;
            int hasHeadBeenReached;
            public TKey Current { get; private set; }

            public Enumerator(ICollection<TKey> keys)
            {
                myEnum = keys.GetEnumerator();
                Current = default;
                myKeys  = keys;
                curr = default;
                hasHeadBeenReached = 0;
            }
            object IEnumerator.Current => Current;
            public bool MoveNext()
            {
                bool ret = false;
                ret = myEnum.MoveNext();
                Current = myEnum.Current;
                return ret;
            }
            public void Dispose()
            {

            }
            public void Reset()
            {

            }
        }
    }
}
