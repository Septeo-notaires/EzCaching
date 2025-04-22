
using EzCache.Cache;
using EzCache.Error;
using EzCache.Tests.Helpers;
using EzCache.Tests.Helpers.Exts;
using Shouldly;

namespace EzCache.Tests;

public class BaseLruCacheTests
{
    protected (LruCache cache, IEnumerable<KeyValuePair<string, string>> values) InitTest(int capacity)
    {
        LruCache cache = new LruCache(capacity);
        IEnumerable<KeyValuePair<string, string>> keys = Enumerable.Range(0, capacity)
            .Select(p => new KeyValuePair<string, string>($"key_{p}", $"value_{p}"));
        
        return (cache, keys);
    }
}

public partial class LruCacheTests : BaseLruCacheTests
{

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_Should_Add_Value_And_Return_It(int capacity)
    {
        //Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);

        //Act
        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        //Assert
        foreach (KeyValuePair<string, string> keyValue in keyValues)
        {
            bool result = cache.TryGetElement(keyValue.Key, out var value);
            string? val = value as string;

            result.ShouldBeTrue();
            val.ShouldBe(keyValue.Value);
        }

        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_Should_Remove_Last_Value_When_Value_Is_Added(int capacity)
    {
        //Arrange
        const string addedNewKey = "added_new_key";
        const string addedNewValue = "added_value";

        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);
        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        //Act
        cache.Add(addedNewKey, addedNewValue);

        //Assert
        bool result = cache.TryGetElement("test_1", out var value);
        result.ShouldBeFalse();
        value.ShouldBeNull();
        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_Should_Return_New_Value_Last_Value_When_Value_Is_Added(int capacity)
    {
        //Arrange
        const string addedNewKey = "added_new_key";
        const string addedNewValue = "added_value";

        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);
        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);
        cache.Add(addedNewKey, addedNewValue);

        //Act
        bool result = cache.TryGetElement(addedNewKey, out object value);
        //Assert
        result.ShouldBeTrue();
        value.ShouldBe(addedNewValue);
        return Task.CompletedTask;
    }

    [Test]
    public Task LruCache_Should_Throw_An_Error_When_We_Add_Same_Key()
    {
        //Arrange
        const string key1 = "key_1";
        const string value1 = "value_1";
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(2);


        //Act
        cache.Add(key1, value1);

        //Assert
        _ = Assert.Throws<KeyAlreadyExistException>(() => cache.Add(key1, value1));
        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_Should_Remove_Value(int capacity)
    {
        //Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);
        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        string removeKey = keyValues.Select(p => p.Key).First();
        cache.Remove(removeKey);
        // Act
        bool result = cache.TryGetElement(removeKey, out var value);

        // Assert
        result.ShouldBeFalse();
        value.ShouldBeNull();
        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_Remove_Should_Decrement_Length(int capacity)
    {
        //Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);
        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        int initLength = cache.GetFieldValue<LruCache, int>("_length");
        string removeKey = keyValues.Select(p => p.Key).First();
        cache.Remove(removeKey);
        // Act
        _ = cache.TryGetElement(removeKey, out var value);

        // Assert
        int length = cache.GetFieldValue<LruCache, int>("_length");
        int cacheCapacity = cache.GetFieldValue<LruCache, int>("_capacity");

        length.ShouldBeLessThan(cacheCapacity);
        length.ShouldBe(initLength - 1);
        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_RemoveLeastUsed_Should_Remove_Last_Node(int capacity)
    {
        // Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);

        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        int initialLength = cache.GetFieldValue<LruCache, int>("_length");
        // Act
        cache.RemoveLeastUsed();

        // Assert
        int length = cache.GetFieldValue<LruCache, int>("_length");
        int cacheCapacity = cache.GetFieldValue<LruCache, int>("_capacity");

        initialLength.ShouldBeLessThanOrEqualTo(cacheCapacity);
        length.ShouldBeLessThan(cacheCapacity);
        length.ShouldBe(--initialLength);

        return Task.CompletedTask;
    }

    [Test]
    [Arguments(10), Arguments(100), Arguments(1000)]
    public Task LruCache_RemoveLeastUsed_Should_Remove_Last_Node_In_List(int capacity)
    {
        // Arrange
        (LruCache cache, IEnumerable<KeyValuePair<string, string>> keyValues) = InitTest(capacity);

        foreach (KeyValuePair<string, string> keyValue in keyValues)
            cache.Add(keyValue.Key, keyValue.Value);

        // Act
        cache.RemoveLeastUsed();

        // Assert
        string lastKey = keyValues.Select(p => p.Key).First();
        bool result = cache.TryGetElement(lastKey, out object value);

        result.ShouldBeFalse();
        value.ShouldBeNull();

        return Task.CompletedTask;
    }
}