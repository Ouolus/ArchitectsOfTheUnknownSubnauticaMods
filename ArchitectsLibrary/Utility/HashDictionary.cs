using System.Collections;
using System.Collections.Generic;
using QModManager.Utility;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// a dictionary that only contains unique keys and overrides the value of a key if exists.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    internal class HashDictionary<K, V> : IDictionary<K, V>
    {
        private readonly Dictionary<K, V> _uniqueDictionary;

        public HashDictionary()
        {
            _uniqueDictionary = new Dictionary<K, V>();
        }

        public V this[K key]
        {
            get => _uniqueDictionary[key];
            set
            {
                if (_uniqueDictionary.ContainsKey(key))
                {
                    Logger.Log(Logger.Level.Warn, $"{key} already exists, value will be overriden.");
                }

                _uniqueDictionary[key] = value;
            }
        }

        public bool IsReadOnly => false;
        public ICollection<K> Keys => _uniqueDictionary.Keys;
        public ICollection<V> Values => _uniqueDictionary.Values;
        public int Count => _uniqueDictionary.Count;

        public void Add(K key, V value)
        {
            if (_uniqueDictionary.ContainsKey(key))
                return;
            
            _uniqueDictionary.Add(key, value);
        }

        public void Add(KeyValuePair<K, V> item) => Add(item.Key, item.Value);

        public bool Remove(K key) => _uniqueDictionary.Remove(key);

        public bool Remove(KeyValuePair<K, V> item) => _uniqueDictionary.Remove(item.Key);

        public bool TryGetValue(K key, out V value) => _uniqueDictionary.TryGetValue(key, out value);

        public void Clear() => _uniqueDictionary.Clear();

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<K, V> kvp in _uniqueDictionary)
                array[arrayIndex++] = kvp;
        }

        public bool Contains(KeyValuePair<K, V> item) => _uniqueDictionary.TryGetValue(item.Key, out V value) && value.Equals(item.Value);

        public IEnumerator GetEnumerator() => _uniqueDictionary.GetEnumerator();

        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() => _uniqueDictionary.GetEnumerator();

        public bool ContainsKey(K key) => _uniqueDictionary.ContainsKey(key);
    }
}