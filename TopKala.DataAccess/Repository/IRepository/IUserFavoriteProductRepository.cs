using TopKala.Models;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUserFavoriteProductRepository : IRepository<UserFavoriteProduct>
    {
        void Update(UserFavoriteProduct favoriteProduct);
    }
}