namespace TopKala.Models
{
    public class ProductInfo : BaseEntity<int>
    {
        public string Value { get; set; }

        #region Relation Properties
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int InfoId { get; set; }
        public Info Info { get; set; }
        #endregion
    }
}