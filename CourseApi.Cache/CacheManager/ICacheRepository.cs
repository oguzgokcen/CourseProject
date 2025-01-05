using System;
using System.Threading.Tasks;

namespace CourseApi.Cache.CacheManager
{
	public interface ICacheRepository
	{
		Task<T?> Get<T>(string key);
		Task Set<T>(string key, T value);
		Task Remove(string key);
	}
}
