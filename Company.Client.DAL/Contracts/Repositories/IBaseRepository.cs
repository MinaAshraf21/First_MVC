using Company.Client.DAL.Common.Entities;
using Company.Client.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Contracts.Repositories
{
    public interface IBaseRepository<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity? Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

    }
}
