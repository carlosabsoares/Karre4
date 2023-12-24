using ControleLancamento.Api.Domain.Repositories;
using ControleLancamento.Api.Infra.Repositories;

namespace ControleLancamento.Api.Api.DependencyMap
{
    public static class RepositoryDependencyMap
    {
        public static void RepositoryMap(this IServiceCollection services)
        {
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }
    }
}