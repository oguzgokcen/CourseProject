using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace CourseApi.Cache.CacheManager
{
	public class CacheRepository(IDistributedCache _cache) : ICacheRepository
	{
		public async Task<T?> Get<T>(string key)
		{
			var cachedData = await _cache.GetStringAsync(key);
			if (cachedData is null)
				return default;

			return JsonSerializer.Deserialize<T>(cachedData);
		}

		public async Task Set<T>(string key, T value)
		{
			var serializedData = JsonSerializer.Serialize(value);
			await _cache.SetStringAsync(key, serializedData);
		}

		public async Task Remove(string key)
		{
			await _cache.RemoveAsync(key);
		}
	}
}
