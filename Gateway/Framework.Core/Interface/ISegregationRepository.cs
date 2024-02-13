using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Interface
{
    // todo repalce to IRepository
    public interface ISegregationRepository<TEntity, TKey> : IDisposable, IAsyncDisposable
        where TEntity : EntityId<TKey>, IAggregateRoot
        where TKey : struct
    {
    }


    public interface IReadRepository<TEntity, TKey> : ISegregationRepository<TEntity, TKey>
     where TEntity : EntityId<TKey>, IAggregateRoot
     where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetList(int skip = 0, int take = 10);
        Task<TEntity?> GetById(TKey id);
        Task<TEntity?> Get(TKey id);


    }

    public interface IQueryRepository<TEntity, TKey> : ISegregationRepository<TEntity, TKey>
    where TEntity : EntityId<TKey>, IAggregateRoot
    where TKey : struct
    {
        IQueryable<TEntity> GetQuerable();
    }

    public interface IWriteRepository<TEntity, TKey> : ISegregationRepository<TEntity, TKey>
        where TEntity : EntityId<TKey>, IAggregateRoot
        where TKey : struct
    {
        Task<TEntity?> Load(TKey id);

        Task Add(TEntity entity, int? createdBy);

        Task Update(TEntity entity, int? updatedBy);
    }


    public interface IDeleteRepository<TEntity, TKey> : ISegregationRepository<TEntity, TKey>
        where TEntity : EntityId<TKey>, IAggregateRoot
        where TKey : struct
    {
        Task Delete(TKey key, int? deletedBy);
    }

    public interface IUsageRepository<TEntity, TKey> : ISegregationRepository<TEntity, TKey>
  where TEntity : EntityId<TKey>, IAggregateRoot
  where TKey : struct
    {
        Task<bool> IsUsage(TKey key);


    }


}
