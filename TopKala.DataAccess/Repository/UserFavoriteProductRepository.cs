using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.DataAccess.Repository
{
    public class UserFavoriteProductRepository : Repository<UserFavoriteProduct>, IUserFavoriteProductRepository
    {

        private readonly ApplicationDbContext _db;

        public UserFavoriteProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserFavoriteProduct favoriteProduct)
        {
            _db.Update(favoriteProduct);
        }
    }
}