namespace KS.Core.Model.Core
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public bool IsCached { get; set; }
    }
}
