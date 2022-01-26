using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightFraud.Application.Common.Interfaces
{
    public interface IAmCaching
    {
        Task<T> TryGetAsync<T>(string key) 
            where T : class;
        Task CreateAsync<T>(string key, T value) 
            where T : class;
        Task CreateAsync<T>(string key, T value, TimeSpan expireTime)
            where T : class;
        Task RemoveAsync(string key);
    }
}
