using ControleLancamento.Api.Domain.Enum;

namespace ControleLancamento.Api.Domain.Entities
{
    public class LancamentoEntity : BaseEntity
    {
        public decimal SaldoInicial { get; set; }

        public decimal Valor { get; set; }

        public EnTipoOperacao TipoOperacao { get; set; }

        public decimal SaldoFinal { get; set; }
    }
}