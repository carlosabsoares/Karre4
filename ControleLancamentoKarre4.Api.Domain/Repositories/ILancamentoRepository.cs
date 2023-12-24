using ControleLancamento.Api.Domain.Entities;

namespace ControleLancamento.Api.Domain.Repositories
{
    public interface ILancamentoRepository : ICudRepository
    {
        Task<LancamentoEntity> FindById(Guid id);

        Task<IList<LancamentoEntity>> FindAll();
    }
}