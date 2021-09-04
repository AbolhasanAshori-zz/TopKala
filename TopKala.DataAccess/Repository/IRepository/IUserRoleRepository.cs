using TopKala.Models;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        void Update(UserRole userRole);
    }
}