namespace EzCache.Policy
{
    internal interface ICachePolicyStrategy
    {
        void AddPolicy();
        void GetPolicy();
        void RemovePolicy();
    }
}
