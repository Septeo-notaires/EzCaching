using System;
using EzCache.Error;
using System.Collections.Generic;
using System.Threading;
using EzCache.Policy;

namespace EzCache.Cache;

public class LruCache
{
    #region Private Variables
    private readonly int _capacity;
    private readonly Mutex _mt = new Mutex();

    private int _length;
    private Dictionary<string, LinkedListNode<ObjectValueCache>> _fastAccess = new Dictionary<string, LinkedListNode<ObjectValueCache>>();
    private LinkedList<ObjectValueCache> _cache = new LinkedList<ObjectValueCache>();
    #endregion Private Variables

    #region Properties
    public int Capacity => _capacity;
    public int Length => _length;
    #endregion Properties


    public LruCache(int capacity) => 
        _capacity = capacity;

    public void Add(string key, ObjectValueCache value)
    {
        _mt.WaitOne();
        if (_length >= _capacity) RemoveLeastUsed();
        if (_fastAccess.ContainsKey(value.Key))
        {
            _mt.ReleaseMutex();
            throw new KeyAlreadyExistException(key);
        }
        
        LinkedListNode<ObjectValueCache> node = _cache.AddFirst(value);
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
        LinkedListNode<ObjectValueCache> val = _fastAccess[key];
        _cache.Remove(val);
        _fastAccess.Remove(val.Value.Key);
        --_length;
        _mt.ReleaseMutex();
    }

    public bool TryGetElement(string key, out ObjectValueCache? valeur)
    {
        valeur = null;
        _mt.WaitOne();
        if (_fastAccess.ContainsKey(key))
        {
            LinkedListNode<ObjectValueCache> node =_fastAccess[key];
            _cache.Remove(node);
            _fastAccess[key] = _cache.AddFirst(node.Value);
            valeur = node.Value;
            _mt.ReleaseMutex();
            return true;
        }
        _mt.ReleaseMutex();
        return false;
    }

    private void RemoveLeastUsed()
    {
        LinkedListNode<ObjectValueCache> lastNode = _cache.Last;
        _length--;
        _cache.Remove(lastNode);
        _fastAccess.Remove(lastNode.Value.Key);
    }

    private IEnumerator<ObjectValueCache> GetEnumerator()
    {
        return _cache.GetEnumerator();
    }
}