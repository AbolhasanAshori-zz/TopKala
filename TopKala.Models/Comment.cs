namespace TopKala.Models
{
    public class Comment : TreeEntity<Comment>
    {
        public string Content { get; set; }

        #region Relation Properties
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}