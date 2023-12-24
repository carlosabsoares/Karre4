using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamento.Api.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<LancamentoEntity> Lancamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.MapLancamento(modelBuilder);
        }
    }
}