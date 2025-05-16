using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EzCache.Policy;

namespace EzCache.Cache;

public class Cache
{
    #region Private Variables
    private readonly LruCache _lruCache;
    private const int DEFAULT_SIZE = 128;
    #endregion Private Variables

    #region  Properties
    public int Capacity => _lruCache.Capacity;
    public float Filled => ((float)_lruCache.Length / (float)_lruCache.Capacity) * 100;
    #endregion Properties

    public Cache() =>
        _lruCache = new LruCache(DEFAULT_SIZE);
    
    public Cache(int capacity) =>
        _lruCache = new LruCache(capacity);

    public void Add(string key, object value, ICachePolicy? policy = null)
    {
        if (policy == null)
        {
            // Default implementation of add operation
        }
    }

    public object? TryGetElement(string key, Func<object> func, ICachePolicy? policy = null)
    {
        if (policy == null)
        {
            // Default retrieve implementation of get operation
        }
        return new();
    }

    /*Add cache stampede*/
    public async Task<object?> TryGetElementAsync(string key, Func<Task<object>> funcAsync, ICachePolicy? policy = null)
    {
        var result = await funcAsync();
        if (policy == null)
        {
            // Default retrieve implementation of get operation
        }
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(string key) =>
        _lruCache.Remove(key);
}