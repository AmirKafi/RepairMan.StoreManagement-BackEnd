using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Interface
{
    public interface IRepository : IDisposable, IAsyncDisposable
    {
    }

    [Obsolete]
    public interface ICrudRepository<T> where T : IAggregateRoot
    {
        Task<IEnumerable<T>> GetList(int skip = 0, int take = 10);
        Task<T> GetById(short id);
        Task<T> Get(short id);
        Task Add(T model);

        //ToDo کاربردش چیه ؟
        Task Update(short id);

        Task Delete(short id);
    }
}
