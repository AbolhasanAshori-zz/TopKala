namespace TopKala.Models
{
    public class ProductColor : BaseEntity<int>
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}