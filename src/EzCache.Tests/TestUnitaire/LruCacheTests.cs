
using EzCache.Cache;
using Shouldly;
using KeyValue = (string key, string value);

namespace EzCache.Tests
{
    public class UnitTest1
    {
        private (LruCache cache, IEnumerable<KeyValue> values) InitTest(int capacity)
        {
            LruCache cache = new LruCache(capacity);
            IEnumerable<KeyValue> keys = Enumerable.Range(0, capacity)
                .Select(p => new KeyValue($"key_{p}", $"value_{p}"));
            
            return (cache, keys);
        }
        
        [Test]
        [Arguments(10), Arguments(100), Arguments(1000)]
        public Task LruCache_Should_Add_Value_And_Return_It(int capacity)
        {
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(capacity);
            foreach (KeyValue keyValue in keyValues)
                cache.Add(keyValue.key, keyValue.value);

            foreach (KeyValue keyValue in keyValues)
            {
                bool result = cache.TryGetElement(keyValue.key, out var value);
                string? val = value as string;
                
                result.ShouldBeTrue();
                val.ShouldBe(keyValue.value);   
            }
            return Task.CompletedTask;
        }

        [Test]
        [Arguments(10), Arguments(100), Arguments(1000)]
        public Task LruCache_ShouldRemove_LastValue_When_Value_Is_Added(int capacity)
        {
            const string addedNewKey = "added_new_key";
            const string addedNewValue = "added_value";
            
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(capacity);
            foreach (KeyValue keyValue in keyValues)
                cache.Add(keyValue.key, keyValue.value);
            
            cache.Add(addedNewKey, addedNewValue);
            
            bool result = cache.TryGetElement("test_1", out var value);
            result.ShouldBeFalse();
            value.ShouldBeNull();
            return Task.CompletedTask;
        }
    }
}