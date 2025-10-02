using Company.Client.DAL.Common.Entities;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Common;
using Company.Client.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity? Get(int id)
        {
            //takes params -> because we probably can deal with an entity with composite PK 
            var entity = _context.Find<TEntity>(id);

            //we should search locally first
            //var entity = _dbSet.Local.FirstOrDefault(x => x.Id == id);
            //if (entity == null)
            //    entity = _dbSet.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            //include nav. properties
            if (includes is not null)
                query = includes(query);

            //applying filter condition  [IQueryable takes Expression<Func>]
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public PaginatedResult<TEntity> GetAll(
            PaginationParameters parameters,
            Expression<Func<TEntity,bool>>? filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? orderBy = null
        )
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);
            if (filter is not null)
                query = query.Where(filter);

            int totalCount = query.Count();
           
            if (orderBy is not null)
                query = orderBy(query);

            // query = _dbSet.Entities.Includes().Where().OrderBy()


            //apply pagination
            var result =  query.Skip((parameters.PageIndex - 1) * parameters.PageSize)
                               .Take(parameters.PageSize)
                               .ToList(); 

            return new PaginatedResult<TEntity>()
            {
                Data = result,
                TotalCount = totalCount,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize
            };
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,bool withTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;
            
            //include nav. properties
            if (includes is not null)
                query = includes(query);

            if (!withTracking)
                return query.AsNoTracking();
                
            return query;
        }
        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Update(TEntity entity) => _context.Update(entity);

        public void Delete(int id)
        {
            var entity = _context.Find<TEntity>(id);

            if (entity != null)
                _dbSet.Remove(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter) 
                                        => _dbSet.Any(filter);

    }
}
