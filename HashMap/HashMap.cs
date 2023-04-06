using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public List<(TKey, TValue)>[] hashList = new List<(TKey, TValue)>[5];
        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count;

        public bool IsReadOnly => throw new NotImplementedException();

        IEqualityComparer<TKey> equalityComparer => EqualityComparer<TKey>.Default;

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(equalityComparer.GetHashCode(key)) % hashList.Length;
            Count++; 
            
            if(hashList.Count >= hashList.Length)
            {
                //increase capacity of list, check that it has enough capacity
                //rehash data in previous list, add all data to new list
                //replace and set new list to previous list

                List<(TKey, TValue)> newHashList = new List<(TKey, TValue)>(hashList.Length * 2);
                for (int i = 0; i < hashList.Count; i++)
                {
                    int temp = Math.Abs(equalityComparer.GetHashCode(hashList[i].Item1)) % newHashList.Capacity;
                    newHashList.Insert(temp, hashList[i]);
                }
                hashList = newHashList;
            }
            hashList.Insert(hash, (key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
