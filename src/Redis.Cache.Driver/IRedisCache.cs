using System.Threading.Tasks;

namespace Redis.Cache.Driver
{
    public interface IRedisCache
    {
        Task SetData<T>(
                    string recordId,
                    T data);

        Task<T> GetData<T>(string recordId);

        Task ClearData(string recordId);
    }
}
