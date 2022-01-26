using FightFraud.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace FightFraud.Application.Common
{
    public static class IAmCachingExtensions
    {
        public static async Task<T> GetOrCacheAsync<T>(
            this IAmCaching me,
            string key,
            Func<Task<T>> func,
            TimeSpan expirationTime = default
            ) where T : class
        {
            var cachedData = await me.TryGetAsync<T>(key);
            if (cachedData != null)
                return cachedData;

            cachedData = await func();
            await me.CreateAsync(key, cachedData, expirationTime);

            return cachedData;
        }
    }
}
