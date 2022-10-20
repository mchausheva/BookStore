using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.Caches.Cache;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.Mongo;
using BookStore.DL.Repositories.MsSQL;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;

namespace BookStore.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonInMemoryRepository>();
            services.AddSingleton<IAuthorRepository, AuthorMsSqlRepository>();
            services.AddSingleton<IBookRepository, BookMsSqlRepository>();
            services.AddSingleton<IEmployeeRpository, EmployeeRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepository>();

            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorService>();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<ProducerService<int, int>>();
            services.AddSingleton<IPurchaseService, PurchaseService>();
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();

            services.AddHostedService<PurchaseConsumer>();
            services.AddHostedService<DeliveryConsumer>();

            return services;
        }

        public static IServiceCollection Subscribe2Cache<TKey, TValue>(this IServiceCollection services) where TValue : ICacheItem<TKey>
        {
            services.AddSingleton<CacheService<TKey, TValue>>();
            services.AddSingleton<CacheConsumer<TKey, TValue>>();

            return services;
        }
    }
}