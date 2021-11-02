using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.DataAccess.Repositories;
using SEDC.PizzaApp.DataAccess.Repositories.CacheRepositories;
using SEDC.PizzaApp.DataAccess.Repositories.DbRepositories;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.BusinessLayer.Helpers
{
    public static class DiModule
    {
        public static IServiceCollection RegisterRepositories(IServiceCollection services)
        {
            // ONE WAY OF REGISTERING DB CONTEXT
            //var connectionString = "Server=(LocalDb)\\MSSqlLocalDb;Database=PizzaDb;Trusted_Connection=True";
            //services.AddDbContext<PizzaAppContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});

            //"Server=(LocalDb)\\MSSqlLocalDb;Database=PizzaDb;Trusted_Connection=True"


            // CacheDb registration
            //services.AddTransient<IRepository<User>, UserRepository>();
            //services.AddTransient<IRepository<Order>, OrderRepository>();
            //services.AddTransient<IRepository<Pizza>, PizzaRepository>();
            //services.AddTransient<IRepository<Feedback>, FeedbackRepository>();

            // Sql server repo registration
            services.AddTransient<IRepository<User>, UserDbRepository>();
            services.AddTransient<IRepository<Order>, OrderDbRepository>(); 
            services.AddTransient<IRepository<Pizza>, PizzaDbRepository>();
            services.AddTransient<IRepository<Feedback>, FeedbackDbRepository>();

            return services;
        }

        public static IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PizzaAppContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
