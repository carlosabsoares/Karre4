using ControleLancamento.Api.Application.Configuration.Commands;
using ControleLancamento.Api.Domain.Enum;
using ControleLancamento.Api.Shared.Extension;
using Flunt.Notifications;
using Flunt.Validations;

namespace ControleLancamento.Api.Application.AppLancamento
{
    public class PutLancamentoCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public EnTipoOperacao TipoOperacao { get; set; }
        public DateTime Data { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(Id.ToString(), "Id", "O Id do cargo é obrigatório")
                    .IsFalse(Data.ToString() == "01/01/0001 00:00:00", "Data", "A data é obrigatória")
                    .IsTrue(Id.ValidationGuid(), "Id", "O Id do cargo está inválido")
                    .IsNullOrNullable(Valor, "Valor", "O valor é obrigatório")
                    .IsFalse(Valor == 0, "Valor", "O valor deve ser maior que zero")
                    .IsTrue(ValidaTipoOperacao(TipoOperacao), "Tipo Operacao", "O tipo de operacao é inválido.")
            );
        }

        private bool ValidaTipoOperacao(EnTipoOperacao tipoOperacao)
        {
            bool _return = false;

            if ((int)tipoOperacao >= Enum.GetValues(typeof(EnTipoOperacao)).Cast<int>().Min() &&
               (int)tipoOperacao <= Enum.GetValues(typeof(EnTipoOperacao)).Cast<int>().Max())
                _return = true;

            return _return;
        }
    }
}