/*
 * Cache Policy configuration
 */

using System;

namespace EzCache.Policy;
public interface ICachePolicy { }

public class HitPolicy : ICachePolicy
{
    #region Private Fields
    private readonly int _maxHit;
    #endregion Private Fields

    internal int MaxHit => _maxHit;

    public HitPolicy(int maxHit) 
        => _maxHit = maxHit;
}

public class TtlPolicy : ICachePolicy
{
    #region Private Fields
    private readonly TimeSpan _ttl;
    private readonly DateTime _endTime;
    #endregion Private Fields

    #region Properties
    public bool Expires => DateTime.Now > _endTime;
    #endregion Properties
    
    public TtlPolicy(TimeSpan ttl)
    {
        _endTime = DateTime.Now + _ttl;
    }
}

public class GroupingPolicy : ICachePolicy
{
    #region Private Fields
    private readonly string _groupName;
    #endregion Private Fields
    
    internal string GroupName => _groupName;
    
    public GroupingPolicy(string groupName)
        => _groupName = groupName;
}