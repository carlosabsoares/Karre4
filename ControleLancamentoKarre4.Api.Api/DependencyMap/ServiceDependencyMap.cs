namespace ControleLancamento.Api.Api.DependencyMap
{
    public static class ServiceDependencyMap
    {
        public static void ServiceMap(this IServiceCollection services, IConfiguration configuration)
        {
            // ----- SERVICES --------
            services.AddHttpClient();

            // Biblioteca para manipulação do JWT
            //services.AddScoped<IJwtService>(sp => new JwtService(configuration.GetValue<string>("APP_KEY")));
        }
    }
}