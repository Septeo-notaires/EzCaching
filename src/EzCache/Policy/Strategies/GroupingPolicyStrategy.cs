namespace EzCache.Policy
{
    internal class GroupingPolicyStrategy : ICachePolicyStrategy    
    {
        void ICachePolicyStrategy.AddPolicy()
        {
            throw new System.NotImplementedException();
        }

       void ICachePolicyStrategy.GetPolicy()
        {
            throw new System.NotImplementedException();
        }

        void ICachePolicyStrategy.RemovePolicy()
        {
            throw new System.NotImplementedException();
        }

        public GroupingPolicyStrategy(ICachePolicy policy)
        {
            GroupingPolicy pol = (GroupingPolicy)policy;
        }
    }
}