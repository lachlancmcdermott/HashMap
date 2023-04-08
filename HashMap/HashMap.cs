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
        public LinkedList<(TKey, TValue)>[] hashList = new LinkedList<(TKey, TValue)>[3];
        private int keyCount = 0;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<TKey> Keys => throw new NotImplementedException();
        public ICollection<TValue> Values => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();
        IEqualityComparer<TKey> equalityComparer => EqualityComparer<TKey>.Default;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(equalityComparer.GetHashCode(key)) % hashList.Length;
            keyCount++;

            if(keyCount > hashList.Length)
            {
                //resize list   
                LinkedList<(TKey, TValue)>[] newList = new LinkedList<(TKey, TValue)>[hashList.Length * 2];
                for (int i = 0; i < hashList.Length; i++)
                {
                    if (hashList[i] != null)
                    {
                        foreach (var item in hashList[i])
                        {
                            int tempHash = Math.Abs(equalityComparer.GetHashCode(hashList[i].Last.Value.Item1)) % hashList.Length;
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
                        hashList[i].RemoveLast();
                    }
                }
                hashList = newList;
            }
            if (hashList[hash] != null && hashList[hash].First != null)
            {
                LinkedListNode<(TKey, TValue)> temp = hashList[hash].Find((key, value));
                if (temp == null)
                {
                    hashList[hash].AddLast(new LinkedListNode<(TKey, TValue)>((key, value)));
                }
                else
                {
                    Exception bad = new Exception("Key/Value combo already exists");
                    throw bad;
                }
            }
            if (hashList[hash] == null)
            {
                hashList[hash] = new LinkedList<(TKey, TValue)>();
                hashList[hash].AddLast(new LinkedListNode<(TKey, TValue)>((key, value)));

            }
        }
        public bool Contains(KeyValuePair<TKey, TValue> Item)
        {
            for (int i = 0; i < hashList.Length; i++)
            {
                if (hashList[i] != null)
                {
                    foreach (var item in hashList[i])
                    {
                        return item.Item1.Equals(Item.Key) && item.Item2.Equals(Item.Value);
                    }
                }
            }
            return false;
        }

        public bool Remove(TKey key)
        {
            int hash = Math.Abs(equalityComparer.GetHashCode(key)) % hashList.Length;
            if (hashList[hash] != null)
            {
                foreach (var item in hashList[hash])
                {
                    if(item.Item1.Equals(key))
                    {
                        hashList[hash].Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Indexing(TKey key)
        {
            throw new Exception("Key does not exist");

        }

        //public int Count()
        //{
        //    int count = 0;
        //    for (int i = 0; i < hashList.Length; i++)
        //    {
        //        for (int k = 0; k < hashList[i].Count; k++)
        //        {
        //            count++;
        //        }
        //    }
        //    return count;
        //}
    }
}
