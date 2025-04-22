using System.Runtime.CompilerServices;
using EzCache.Cache;

namespace EzCache.Tests.Helpers.Exts;

public static class LruCacheExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveLeastUsed(this LruCache instance) =>
        instance.ExecutePrivateMethod("RemoveLeastUsed");
}