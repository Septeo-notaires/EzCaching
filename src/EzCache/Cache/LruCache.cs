using EzCache.Error;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EzCache.Cache
{
    public class LruCache
    {
        #region Private Variables
        private readonly int _capacity;
        private readonly Mutex _mt = new Mutex();

        private int _lenght;
        private Dictionary<string, LinkedListNode<ObjectValue>> _fastAccess = new Dictionary<string, LinkedListNode<ObjectValue>>();
        private LinkedList<ObjectValue> _cache = new LinkedList<ObjectValue>();
        #endregion Private Variables

        #region Properties 
        #endregion Properties

        struct ObjectValue
        {
            public string Key { get; private set; }
            public object Value { get; private set; }

            public ObjectValue(string key, object value)
            {
                Key = key;
                Value = value;
            }
        }

        public LruCache(int capacity, bool cap) : this(capacity)
        {
        }

        public LruCache(int capacity) => 
            _capacity = capacity;

        public void Add(string key, object value)
        {
            _mt.WaitOne();
            if (_lenght >= _capacity) RemoveLeastUsed();
            if (_fastAccess.ContainsKey(key))
            {
                _mt.ReleaseMutex();
                throw new KeyAlreadyExistException(key);
            }

            LinkedListNode<ObjectValue> node = _cache.AddFirst(new ObjectValue(key, value));
            _fastAccess.Add(key, node);
            _lenght++;
            _mt.ReleaseMutex();
        }


        public void Remove(string key)
        {
            _mt.WaitOne();
            if (!_fastAccess.ContainsKey(key))
            {
                _mt.ReleaseMutex();
                throw new InvalidOperationException("");
            }
            LinkedListNode<ObjectValue> val = _fastAccess[key];
            _cache.Remove(val);
            _fastAccess.Remove(val.Value.Key);
            --_lenght;
            _mt.ReleaseMutex();
        }

        public bool TryGetElement(string key, out object valeur)
        {
            valeur = default;
            _mt.WaitOne();
            if (_fastAccess.ContainsKey(key))
            {
                LinkedListNode<ObjectValue> node =_fastAccess[key];
                _cache.Remove(node);
                _fastAccess[key] = _cache.AddFirst(node.Value);
                valeur = node.Value.Value;
                _mt.ReleaseMutex();
                return true;
            }
            _mt.ReleaseMutex();
            return false;
        }

        private void RemoveLeastUsed()
        {
            LinkedListNode<ObjectValue> lastNode = _cache.Last;
            _lenght--;
            _cache.Remove(lastNode);
            _fastAccess.Remove(lastNode.Value.Key);
        }
    }
}
