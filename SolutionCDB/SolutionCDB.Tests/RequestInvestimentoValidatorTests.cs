using FluentValidation.TestHelper;
using NUnit.Framework;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Service.Validators;

namespace SolutionCDB.Tests
{

    [TestFixture]
    public class RequestInvestimentoValidatorTests
    {
        private RequestInvestimentoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RequestInvestimentoValidator();
        }

        [Test]
        public void Deve_Falhar_Quando_ValorInvestimento_For_Menor_Que_Zero()
        {
            var model = new RequestInvestimento { ValorInvestimento = -100, PrazoMes = 12 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ValorInvestimento);
        }

        [Test]
        public void Deve_Falhar_Quando_PrazoMes_For_Menor_Que_Um()
        {
            var model = new RequestInvestimento { ValorInvestimento = 1000, PrazoMes = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMes);
        }

        [Test]
        public void Deve_Passar_Quando_Dados_Sao_Validos()
        {
            var model = new RequestInvestimento { ValorInvestimento = 1000, PrazoMes = 12 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
