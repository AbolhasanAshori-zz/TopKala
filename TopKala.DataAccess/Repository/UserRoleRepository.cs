using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.DataAccess.Repository
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserRole userRole)
        {
            _db.Update(userRole);
        }
    }
}