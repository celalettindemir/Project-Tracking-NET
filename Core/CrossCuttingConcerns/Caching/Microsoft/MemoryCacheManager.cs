using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache? _cache;
        public MemoryCacheManager() 
        {
            _cache= ServiceTool.ServiceProvider.GetService(typeof(IMemoryCache)) as IMemoryCache;
        }
        public void Add(string key, object value, int duration) => _cache.Set(key, value, TimeSpan.FromMinutes(duration));

        public T Get<T>(string key) => _cache.Get<T>(key);

        public object Get(string key) => Get<object>(key);

        public bool IsAdd(string key) => _cache?.TryGetValue(key, out _) ?? false;

        public void Remove(string key) => _cache?.Remove(key);

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(_cache) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            if (cacheEntriesCollection == null)
                return;

            foreach (var cacheItem in cacheEntriesCollection)
            {

                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);


                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
        }
    }
}
