using EzCache.Cache;
using Shouldly;

namespace EzCache.Tests;

public class LruCacheTestsAsync : BaseLruCacheTests
{
    [Test]
    [MatrixDataSource]
    public async Task LruCache_AddCache_In_Parallel(
        [Matrix<int>(10, 100, 1000)] int capacity,
        [Matrix<int>(0, 10, 50)] int delay,
        CancellationToken token)
    {
        // Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValue) = InitTest(capacity);
        
        // Act
        await Parallel.ForEachAsync(keyValue, async (i, _) =>
        {
            await Task.Delay(delay);
            cache.Add(i.Key, i.Value);
        });
        
        // Assert
        foreach (KeyValuePair<string, string> keyVal in keyValue)
        {
            bool result = cache.TryGetElement(keyVal.Key, out object value);
            result.ShouldBeTrue();
            value.ShouldNotBeNull();
            value.ShouldBeOfType<string>();
            value.ShouldBe(keyVal.Value);
        }
    }
    
    [Test]
    [MatrixDataSource]
    [Timeout(10000)]
    public async Task LruCache_AddCache_In_Concurently(
        [Matrix<int>(10, 100, 1000)] int capacity,
        [Matrix<int>(0, 10, 50)] int delay,
        CancellationToken token)
    {
        // Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValue) = InitTest(capacity);
        
        // Act
        await Task.WhenAll(
                keyValue.Select(
                    p => Task.Run(
                        async () =>
                        {
                            await Task.Delay(delay);
                            cache.Add(p.Key, p.Value);
                        })
                    )
            );
        
        // Assert
        foreach (KeyValuePair<string, string> keyVal in keyValue)
        {
            bool result = cache.TryGetElement(keyVal.Key, out object value);
            result.ShouldBeTrue();
            value.ShouldNotBeNull();
            value.ShouldBeOfType<string>();
            value.ShouldBe(keyVal.Value);
        }
    }
}