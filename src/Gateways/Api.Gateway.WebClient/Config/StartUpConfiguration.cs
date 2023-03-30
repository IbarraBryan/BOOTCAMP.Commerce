using Api.Gateway.Proxies;

namespace Api.Gateway.WebClient.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppsettingBinding(this IServiceCollection services, IConfiguration configuration) { 
            services.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
            return services;
        }

        public static IServiceCollection AddProxiesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient<IOrderProxy, OrderProxy>();
            services.AddHttpClient<ICustomerProxy, CustomerProxy>();
            services.AddHttpClient<ICatalogProxy, CatalogProxy>();

            return services;
        }
    }
}
