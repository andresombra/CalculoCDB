using FluentValidation;
using SolutionCDB.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Domain.Validator
{
    public class RequestInvestimentoValidator : AbstractValidator<RequestInvestimento>
    {
        public RequestInvestimentoValidator()
        {
            RuleFor(x => x.ValorInvestimento).NotNull();
            RuleFor(x => x.ValorInvestimento)
                .GreaterThan(0)
                .WithMessage("O valor de investimento deve ser maior que zero.");

            RuleFor(x => x.PrazoMes).NotNull();
            RuleFor(x => x.PrazoMes)
                .GreaterThan(1)
                .WithMessage("O prazo mes deve ser maior que 1.");

        }
    }
}
