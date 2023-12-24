namespace ControleLancamento.Api.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataCriacao { get; set; }

        public BaseEntity()
        {
            DataCriacao = DateTime.Now;
        }
    }
}