using Company.Client.DAL.Common.Entities;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (!withTracking)
                return _dbSet.AsNoTracking();

            return _dbSet;
        }
        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Update(TEntity entity) => _context.Update(entity);

        public void Delete(int id)
        {
            var entity = _context.Find<TEntity>(id);

            if (entity != null)
                _dbSet.Remove(entity);
        }


    }
}
