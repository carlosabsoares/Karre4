using AutoMapper;
using ControleLancamento.Api.Application.Configuration.Mapper;

namespace ControleLancamento.Api.Api.DependencyMap
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var config = AutoMapperConfig.RegisterMapper();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}