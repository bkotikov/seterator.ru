﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Rauthor.Services
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавляет сервис фильтрации нецензурной брани.
        /// </summary>
        /// <param name="replaceChar">Символ, на который следует заменять символы нецензурных слов.</param>
        public static IServiceCollection AddFoulLanguageFilter(this IServiceCollection services, string replaceChar)
        {
            return services.AddSingleton(x => new FoulLanguageFilter(replaceChar));
        }

        public static IServiceCollection AddPrimitiveMemoryCache(this IServiceCollection services)
        {
            return services.AddSingleton<IMemoryCache>(new MemoryCache());
        }
    }
}
