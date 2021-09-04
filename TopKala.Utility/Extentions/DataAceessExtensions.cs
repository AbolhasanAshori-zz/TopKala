using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TopKala.Utility.Extentions
{
    public static class DataAceessExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            
            return query;
        }
    }
}