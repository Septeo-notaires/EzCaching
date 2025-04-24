using System;
using EzCache.Error;
using System.Collections.Generic;
using System.Threading;
using EzCache.Policy;

namespace EzCache.Cache
{
    public class LruCache
    {
        #region Private Variables
        private readonly int _capacity;
        private readonly Mutex _mt = new Mutex();

        private int _length;
        private Dictionary<string, LinkedListNode<ObjectValue>> _fastAccess = new Dictionary<string, LinkedListNode<ObjectValue>>();
        private LinkedList<ObjectValue> _cache = new LinkedList<ObjectValue>();
        #endregion Private Variables

        #region Properties 
        #endregion Properties

        struct ObjectValue
        {
            public ICachePolicyStrategy CacheStrategy { get; private set; }
            public string Key { get; private set; }
            public object Value { get; private set; }

            public ObjectValue(string key, object value, ICachePolicyStrategy cacheStrategy = null)
            {
                Key = key;
                Value = value;
                CacheStrategy = cacheStrategy;
            }
        }

        public LruCache(int capacity, bool cap) : this(capacity)
        {
            
        }

        public LruCache(int capacity) => 
            _capacity = capacity;

        public void Add(string key, object value, ICachePolicy policy = null)
        {
            _mt.WaitOne();
            if (_length >= _capacity) RemoveLeastUsed();
            if (_fastAccess.ContainsKey(key))
            {
                _mt.ReleaseMutex();
                throw new KeyAlreadyExistException(key);
            }

            ObjectValue objValue = policy switch
            {
                null => new ObjectValue(key, value),
                HitPolicy => new ObjectValue(key, value, new HitPolicyStrategy(policy)),
                TtlPolicy => new ObjectValue(key, value, new TtlPolicyStrategy(policy)),
                GroupingPolicy => new ObjectValue(key, value, new GroupingPolicyStrategy(policy)),
                _ => throw new NotImplementedException()
            };

            LinkedListNode<ObjectValue> node = _cache.AddFirst(objValue);
            _fastAccess.Add(key, node);
            _length++;
            _mt.ReleaseMutex();
        }


        public void Remove(string key)
        {
            _mt.WaitOne();
            if (!_fastAccess.ContainsKey(key))
            {
                _mt.ReleaseMutex();
                throw new KeyNotFoundException(key);
            }
            LinkedListNode<ObjectValue> val = _fastAccess[key];
            _cache.Remove(val);
            _fastAccess.Remove(val.Value.Key);
            --_length;
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
            _length--;
            _cache.Remove(lastNode);
            _fastAccess.Remove(lastNode.Value.Key);
        }
    }
}
