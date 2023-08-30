﻿using Core.Persistance.Dynamic;
using Core.Persistance.Models;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistance.Repositories
{
    public interface IAsyncRepository<TEntity, TEntityId>: IQuery<TEntity>
        where TEntity : Entity<TEntityId>
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
            );

        Task<Paginate<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
            );

        Task<Paginate<TEntity>> GetListByDynamicAsync(
           DynamicQuery dynamic,
           Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           int index = 0,
           int size = 10,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
           );

        Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate, bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity,bool permanent = false);

        Task<ICollection<TEntity>> BulkInsertAsync(ICollection<TEntity> entities);
        Task<ICollection<TEntity>> BulkUpdateAsync(ICollection<TEntity> entities);
    }
}
