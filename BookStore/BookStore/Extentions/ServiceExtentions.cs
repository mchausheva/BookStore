using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;

namespace BookStore.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonInMemoryRepository>();
            services.AddSingleton<IAuthorRepository, AuthorInMemoryRepository>();
            services.AddSingleton<IBookRepositry, BookInMemoryRepositry>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorService>();
            services.AddSingleton<IBookService, BookService>();

            return services;
        }
    }
}
