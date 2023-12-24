namespace ControleLancamento.Api.Domain.Entities
{
    public class ConsolidadoDto
    {
        public DateTime Data { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal SaldoFinal { get; set; }
    }
}