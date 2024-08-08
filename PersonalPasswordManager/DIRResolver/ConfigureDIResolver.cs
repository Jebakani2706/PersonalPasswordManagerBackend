using PersonalPasswordManager.Repository;
using PersonalPasswordManager.Service;

namespace PersonalPasswordManager.DIRResolver
{
    public static class ConfigureDIResolver
    {
        public static void PMDIResolver(this IServiceCollection services)
        {
            services.AddScoped<PasswordManagerService>();
            services.AddScoped<IPasswordManagerRepository, PasswordManagerRepository>();
        }
    }
}
