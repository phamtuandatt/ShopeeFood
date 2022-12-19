using Newtonsoft.Json;
using Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {
        public async Task<T> AddObjectAsync(string URL, T entity)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(URL, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return entity;
                    }
                }
                return null;
            }
        }

        public async Task<bool> DeleteObjectAsync(string URL, T entity)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(URL))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public async Task<T> GetObjectAsync(string URL, Func<T, bool> condition)
        {
            return null;
        }

        public Task<IAsyncEnumerable<T>> GetObjectsAsync(string URL, Func<T, IEnumerable<T>> condition)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateObjectAsync(string URL, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
