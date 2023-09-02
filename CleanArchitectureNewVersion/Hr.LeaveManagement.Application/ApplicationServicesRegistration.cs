using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hr.LeaveManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
