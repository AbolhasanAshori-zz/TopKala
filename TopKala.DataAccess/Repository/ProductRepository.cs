using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}