
using FluentValidation;
using SolutionCDB.Domain.DTO;

namespace SolutionCDB.Service.Validators
{

    public class RequestInvestimentoValidator : AbstractValidator<RequestInvestimento>
    {
        public RequestInvestimentoValidator()
        {
            RuleFor(x => x.ValorInvestimento)
                .GreaterThan(0).WithMessage("O valor do investimento deve ser maior que zero.");

            RuleFor(x => x.PrazoMes)
                .GreaterThan(0).WithMessage("O prazo em meses deve ser maior que zero.");
        }
    }

}
