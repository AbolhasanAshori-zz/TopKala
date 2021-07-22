using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopKala.Models;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        void Update(UserRole userRole);
    }
}