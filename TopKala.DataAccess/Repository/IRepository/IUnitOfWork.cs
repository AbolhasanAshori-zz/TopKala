using System;

namespace TopKala.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        IUserFavoriteProductRepository UserFavoriteProduct { get; }
        void Save();
    }
}