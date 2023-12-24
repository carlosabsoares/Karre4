using ControleLancamento.Api.Application.Configuration.Queries;
using Flunt.Notifications;
using Flunt.Validations;

namespace ControleLancamento.Api.Application.AppConsolidado
{
    public class GetByDataConsolidadoQuery : Notifiable, IQuery
    {
        public DateTime Data { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsFalse(Data.ToString() == "01/01/0001 00:00:00", "Data", "A data é obrigatória"));
        }
    }
}