using FightFraud.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System;

namespace FightFraud.Infrastructure.Caching
{
    /// <summary>
    /// This is just a sample implementation wrapper for the sake of this challenge.
    /// As we are using IDistributedCache, we could easily later 
    /// use a proper caching provider like Redis.
    /// </summary>
    public class InMemoryCaching : IAmCaching
    {
        private readonly IDistributedCache _cachingProvider;

        public InMemoryCaching(IDistributedCache cachingProvider)
        {
            _cachingProvider = cachingProvider;
        }

        public async Task<T> TryGetAsync<T>(string key)
            where T : class
        {
            return await _cachingProvider.GetJsonObjectAsync<T>(key);
        }

        public async Task CreateAsync<T>(string key, T value)
            where T : class
        {
            await _cachingProvider.SetJsonObjectAsync(key, value);
        }

        public async Task CreateAsync<T>(string key, T value, TimeSpan expireTime)
            where T : class
        {
            await _cachingProvider.SetJsonObjectAsync(key, value, expireTime);
        }

        public async Task RemoveAsync(string key)
        {
            await _cachingProvider.RemoveAsync(key);
        }
    }
}