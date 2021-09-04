using TopKala.Models;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}