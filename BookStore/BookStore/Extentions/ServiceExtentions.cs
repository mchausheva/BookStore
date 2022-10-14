using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.Caches.Cache;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MsSQL;
using BookStore.Models.Models;
using Newtonsoft.Json.Linq;

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
            services.AddSingleton<CacheService<int, Book>>();

            return services;
        }
    }
}
