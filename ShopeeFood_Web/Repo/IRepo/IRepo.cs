using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.IRepo
{
    public interface IRepo<T> where T : class
    {
        Task<IAsyncEnumerable<T>> GetObjectsAsync(string URL, Func<T , IEnumerable<T>> condition);

        Task<T> GetObjectAsync(string URL, Func<T, bool> condition);
        
        Task<T> AddObjectAsync(string URL, T entity);

        Task<T> UpdateObjectAsync(string URL, T entity);

        Task<bool> DeleteObjectAsync(string URL, int Id);
    }
}
