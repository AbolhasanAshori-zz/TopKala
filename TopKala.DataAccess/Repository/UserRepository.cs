using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            _db.Update(user);
        }
    }
}