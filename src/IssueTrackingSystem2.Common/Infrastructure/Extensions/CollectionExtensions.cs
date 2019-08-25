namespace IssueTrackingSystem2.Common.Infrastructure.Extensions
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static void AddRange<T>(this IList<T> collection, params T[] items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
