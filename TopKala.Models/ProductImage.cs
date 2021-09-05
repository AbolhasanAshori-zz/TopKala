namespace TopKala.Models
{
    public class ProductImage : BaseEntity<int>
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}