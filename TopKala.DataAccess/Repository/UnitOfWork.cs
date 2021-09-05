using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;

namespace TopKala.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            User = new UserRepository(_db);
            UserRole = new UserRoleRepository(_db);
            UserFavoriteProduct = new UserFavoriteProductRepository(_db);
        }

        public IProductRepository Product { get; private set; }

        public IUserRepository User { get; private set; }

        public IUserRoleRepository UserRole { get; private set; }
        public IUserFavoriteProductRepository UserFavoriteProduct { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}