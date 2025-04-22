
using EzCache.Cache;
using EzCache.Error;
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
            //Arrange
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(capacity);
            
            //Act
            foreach (KeyValue keyValue in keyValues)
                cache.Add(keyValue.key, keyValue.value);

            //Assert
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
        public Task LruCache_Should_Remove_Last_Value_When_Value_Is_Added(int capacity)
        {
            //Arrange
            const string addedNewKey = "added_new_key";
            const string addedNewValue = "added_value";
            
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(capacity);
            foreach (KeyValue keyValue in keyValues)
                cache.Add(keyValue.key, keyValue.value);
            
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
            
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(capacity);
            foreach (KeyValue keyValue in keyValues)
                cache.Add(keyValue.key, keyValue.value);
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
            (LruCache cache, IEnumerable<KeyValue> keyValues) = InitTest(2);
            
            
            //Act
            cache.Add(key1, value1);
            
            //Assert
            _ = Assert.Throws<KeyAlreadyExistException>(() => cache.Add(key1, value1));
            return Task.CompletedTask;
        }
    }
}