using System.Collections.Generic;

namespace TopKala.Models
{
    public class TreeEntity<T> : BaseEntity<int> 
        where T : class
    {
        public int? ParentId { get; set; }
        public T Parent { get; set; }
        public IEnumerable<T> Children { get; set; }
    }
}