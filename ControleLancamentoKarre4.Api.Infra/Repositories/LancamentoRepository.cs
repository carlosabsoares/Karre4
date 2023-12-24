using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Repositories;
using ControleLancamento.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamento.Api.Infra.Repositories
{
    public class LancamentoRepository : CudRepository, ILancamentoRepository
    {
        private readonly DataContext _context;

        public LancamentoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<LancamentoEntity>> FindAll()
        {
            return await _context.Lancamentos.AsNoTracking().ToListAsync();
        }

        public async Task<LancamentoEntity> FindById(Guid id)
        {
            return await _context.Lancamentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}