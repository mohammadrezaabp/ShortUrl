using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UrlShortner.Repository;
using UrlShortner.Repository.Abstract;
using UrlShortner.Repository.Concrete;
using UrlShortner.Service.Abstract;
using UrlShortner.Service.Concerete;

namespace UrlShortner.Service
{
    public class DI
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {


            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Inspection.Repository")));
            ResolveAllTypes(services, ServiceLifetime.Transient, typeof(IService), "Service");
            ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(IRepository<>), "Repository");

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IService<,>), typeof(Service<,>));

        }

        public static void ResolveAllTypes(IServiceCollection services, ServiceLifetime serviceLifetime, Type refType, string suffix)
        {
            var assembly = refType.GetTypeInfo().Assembly;

            var allServices = assembly.GetTypes().Where(t =>
                t.GetTypeInfo().IsClass &&
                !t.GetTypeInfo().IsAbstract &&
                !t.GetType().IsInterface &&
                //(!t.Name.StartsWith("I") ||) &&
                t.Name.EndsWith(suffix)
            );


            foreach (var type in allServices)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Except
                    (allInterfaces.SelectMany(t => t.GetInterfaces()));
                foreach (var itype in mainInterfaces)
                {
                    if (allServices.Any(x => !x.Equals(type) && itype.IsAssignableFrom(x)))
                    {
                        throw new Exception("The " + itype.Name +
                                            " type has more than one implementations, please change your filter");
                    }
                    services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
                }
            }
        }


    }
}
