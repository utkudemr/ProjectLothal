using Core.Persistance.Dynamic;
using Core.Persistance.Models;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Persistance.Repositories
{
    public class EfRepositoryBase<TEntity, TEntityId, TContext> : IAsyncRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
        where TContext : DbContext
    {
        protected readonly TContext context;
        public EfRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<TEntity>> BulkInsertAsync(ICollection<TEntity> entities)
        {
            entities = entities.Select(a => { a.CreatedDate = DateTime.Now; return a; }).ToList();
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task<ICollection<TEntity>> BulkUpdateAsync(ICollection<TEntity> entities)
        {
            entities = entities.Select(a => { a.ModifiedDate = DateTime.Now; return a; }).ToList();
            context.UpdateRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            if (permanent == false)
            {
                EntityHasOneToOneRelation(entity);
                await SetEntitySoftDeletedAsync(entity);
                await context.SaveChangesAsync();
            }
               
            else
            {
                context.Remove(entity);
            }
            return entity;
        }

        protected void EntityHasOneToOneRelation(TEntity entity)
        {
            bool hasEntityOneToOneRelation = context.Entry(entity).Metadata.GetForeignKeys()
                .All(
                a => a.DependentToPrincipal?.IsCollection == true
                || a.PrincipalToDependent?.IsCollection == true
                || a.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()) == false;
            if (hasEntityOneToOneRelation) throw new ArgumentException("You can not soft delete when entity has one to one relation");
        }

        protected async Task SetEntitySoftDeletedAsync(IEntityTimestamps entity)
        {
            if (entity.DeletedDate.HasValue)
                return;
            entity.DeletedDate = DateTime.UtcNow;

            var navigations = context
                .Entry(entity)
                .Metadata.GetNavigations()
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();
            foreach (INavigation? navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned())
                    continue;
                if (navigation.PropertyInfo == null)
                    continue;

                object? navValue = navigation.PropertyInfo.GetValue(entity);
                if (navigation.IsCollection)
                {
                    if (navValue == null)
                    {
                        IQueryable query = context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToListAsync();
                        if (navValue == null)
                            continue;
                    }

                    foreach (IEntityTimestamps navValueItem in (IEnumerable)navValue)
                        await SetEntitySoftDeletedAsync(navValueItem);
                }
                else
                {
                    if (navValue == null)
                    {
                        IQueryable query = context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                            .FirstOrDefaultAsync();
                        if (navValue == null)
                            continue;
                    }

                    await SetEntitySoftDeletedAsync((IEntityTimestamps)navValue);
                }
            }

            context.Update(entity);
        }


        protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
        {
            Type queryProviderType = query.Provider.GetType();
            MethodInfo createQueryMethod =
                queryProviderType
                    .GetMethods()
                    .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                    ?.MakeGenericMethod(navigationPropertyType)
                ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
            var queryProviderQuery =
                (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
            return queryProviderQuery.Where(x => !((IEntityTimestamps)x).DeletedDate.HasValue);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Query();
            if (enableTracking == false)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (withDeleted)
                query = query.IgnoreQueryFilters();
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Query();
            if (enableTracking == false)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (withDeleted)
                query = query.IgnoreQueryFilters();
            if (predicate!=null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToPaginateAsync(index, size, cancellationToken);
            return await query.ToPaginateAsync(index, size, cancellationToken);

        }

        public async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Query().ToDynamic(dynamic);
            if (enableTracking == false)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (withDeleted)
                query = query.IgnoreQueryFilters();
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToPaginateAsync(index, size, cancellationToken);
            return await query.ToPaginateAsync(index, size, cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Query();
            if (enableTracking == false)
                query = query.AsNoTracking();
            if (withDeleted)
                query = query.IgnoreQueryFilters(); // with deleted for global filters.
            if (predicate != null)
                query = query.Where(predicate);

            var result = await query.AnyAsync(cancellationToken);
            return result;
        }

        public IQueryable<TEntity> Query()
        {
            return context.Set<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
