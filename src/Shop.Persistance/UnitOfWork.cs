﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Entities;
using ShoesShop.Persistence.Repository;

namespace ShoesShop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly bool useServices;
        private readonly ShopDbContext dbContext;

        public UnitOfWork(ShopDbContext context, bool useServices = true)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.useServices = useServices;
        }

        private IRepositoryOf<T>? GetRepositoryOf<T>() where T : class
        {
            return typeof(T) switch
            {
                Type t when t == typeof(Address) => new AddressRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(ShopCartItem) => new ShopCartItemRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(FavoritesItem) => new FavoritesItemRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(Image) => new ImageRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(Model) => new ModelRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(ModelSize) => new ModelSizeRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(ModelVariant) => new ModelVariantRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(Order) => new OrderRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(OrderItem) => new OrderItemRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(Review) => new ReviewRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(User) => new UserRepository(dbContext) as IRepositoryOf<T>,
                Type t when t == typeof(Sale) => new SaleRepository(dbContext) as IRepositoryOf<T>,
                _ => null,
            };
        }

        public IRepositoryOf<T> GetRepositoryOf<T>(bool ignoreServices = false) where T : class
        {
            if (useServices && !ignoreServices)
            {
                return dbContext.GetService<IRepositoryOf<T>>();
            }
            return GetRepositoryOf<T>() ?? new GenericRepository<T>(dbContext);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed || disposing)
            {
                dbContext?.Dispose();
            }
            _disposed = true;
        }
    }
}
