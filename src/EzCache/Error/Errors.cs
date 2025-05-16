using System;

namespace EzCache.Error;
public class KeyAlreadyExistException: Exception
{
    private readonly string _keyName;
    public KeyAlreadyExistException() { }
    public KeyAlreadyExistException(string key) =>
        _keyName = key;

    public override string ToString() =>
        $"Key {_keyName} already exist you can't add the same key";
}