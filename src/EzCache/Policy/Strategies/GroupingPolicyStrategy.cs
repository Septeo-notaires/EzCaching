using System;

namespace EzCache.Policy;

internal class GroupingPolicyStrategy : ICachePolicyStrategy    
{
    public GroupingPolicyStrategy(ICachePolicy policy)
    {
        GroupingPolicy pol = (GroupingPolicy)policy;
    }
    public void GetPolicy()
    {
        throw new System.NotImplementedException();
    }

    public void RemovePolicy()
    {
        throw new System.NotImplementedException();
    }
}