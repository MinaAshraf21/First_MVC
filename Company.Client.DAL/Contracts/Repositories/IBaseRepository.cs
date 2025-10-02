using Company.Client.DAL.Common.Entities;
using Company.Client.DAL.Persistence.Common;
using System.Linq.Expressions;

namespace Company.Client.DAL.Contracts.Repositories
{
    public interface IBaseRepository<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null, bool withTracking = false);
        PaginatedResult<TEntity> GetAll(
            PaginationParameters parameters,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
            Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>>? orderBy = null);

        TEntity? Get(int id);
        TEntity? Get(Expression<Func<TEntity,bool>> filter , 
                    Func<IQueryable<TEntity> , IQueryable<TEntity>>? includes = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}
