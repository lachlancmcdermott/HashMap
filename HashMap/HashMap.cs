using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HashMap
{
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #nullable enable
        public LinkedList<(TKey, TValue)>[] buckets = new LinkedList<(TKey, TValue)>[3];

        public TValue this [TKey key]
        {
            get
            {
                int hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
                for (int i = 0; i < buckets[hash].Count; i++)
                {
                    LinkedListNode<(TKey, TValue)> curr = buckets[hash].First;
                    if (curr.Next.Value.Item1.Equals(key))
                    {
                        return curr.Value.Item2;
                    }
                }
                throw new Exception("Key doesn't exist in hash map");
            }
            set
            {
                bool exists = false;
                int hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
                LinkedListNode<(TKey, TValue)> curr = buckets[hash].First;

                for (int i = 0; i < buckets[hash].Count; i++)
                {
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
        public ICollection<TKey> Keys => throw new NotImplementedException();
        public ICollection<TValue> Values => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();
        IEqualityComparer<TKey> equalityComparer => EqualityComparer<TKey>.Default;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(equalityComparer.GetHashCode(key)) % buckets.Length;
            keyCount++;
            
            if(keyCount > buckets.Length)
            {
                //resize list   
                LinkedList<(TKey, TValue)>[] newList = new LinkedList<(TKey, TValue)>[buckets.Length * 2];
                for (int i = 0; i < buckets.Length; i++)
                {
                    //if (buckets[i] != null)
                    //{
                        foreach (var item in buckets[i])
                        {
                            int tempHash = Math.Abs(equalityComparer.GetHashCode(buckets[i].Last.Value.Item1)) % buckets.Length;
                            if (newList[tempHash] == null)
                            {
                                newList[tempHash] = new LinkedList<(TKey, TValue)>();
                                newList[tempHash].AddLast(new LinkedListNode<(TKey, TValue)>((item.Item1, item.Item2)));
                            }
                            else
                            {
                                newList[tempHash].AddLast(new LinkedListNode<(TKey, TValue)>((item.Item1, item.Item2)));
                            }
                        }
                        buckets[i].RemoveLast();
                    //}
                }
                buckets = newList;
            }
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
                buckets[hash] = new LinkedList<(TKey, TValue)>();
                buckets[hash].AddLast(new LinkedListNode<(TKey, TValue)>((key, value)));

            }
        }
        public bool Contains(KeyValuePair<TKey, TValue> Item)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                {
                    foreach (var item in buckets[i])
                    {
                        return item.Item1.Equals(Item.Key) && item.Item2.Equals(Item.Value);
                    }
                }
            }
            return false;
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
        public bool Resize()
        {
            return false;
        }
        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }
        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }
        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotImplementedException();
        }
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
