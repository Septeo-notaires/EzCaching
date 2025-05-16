using System;

namespace EzCache.Policy;

internal class GroupingPolicyStrategy : ICachePolicyStrategy    
{
    #region Private Fields
    private readonly GroupingPolicy _policy;
    #endregion Private Fields
    public GroupingPolicyStrategy(ICachePolicy policy)
    {
        _policy = (GroupingPolicy)policy;
    }

    public bool GetPolicy() 
        => true;
    
    public void RemovePolicy()
    {
        throw new System.NotImplementedException();
    }
}