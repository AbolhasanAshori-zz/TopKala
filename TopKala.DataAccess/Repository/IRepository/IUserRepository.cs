using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopKala.Models;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}