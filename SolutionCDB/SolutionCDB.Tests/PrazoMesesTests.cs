using FluentValidation.TestHelper;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Service.Service;
using SolutionCDB.Service.Validators;
namespace SolutionCDB.Tests
{
    [TestFixture]
    public class TestePrazoMeses
    {
        CdbService _cdbService;
        RequestInvestimentoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _cdbService = new CdbService();
            _validator = new RequestInvestimentoValidator();
        }

        [Test]
        [TestCase(1000, 6, 1059.7556770148981, 1046.310649686546)]
        [TestCase(1000, 12, 1123.0820949653053, 1098.4656759722443)]
        [TestCase(1000, 24, 1261.31339203165, 1215.5835484261183)]

        public void CalcularCdbDadosBasicos(double valorInvestimento, int prazoMes, double valorBruto, double valorLiquido)
        {

            var request = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes = prazoMes };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);

            Assert.Greater(response.ValorLiquido, 0);
            Assert.Greater(response.ValorBruto, 0);

            double tolerance = 0.0001;

            Assert.That(response.ValorBruto, Is.EqualTo(valorBruto).Within(tolerance));
            Assert.That(response.ValorLiquido, Is.EqualTo(valorLiquido).Within(tolerance));
        }

        [Test]
        public void CalcularCdbAcimaDe24meses()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 36 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);

            Assert.Greater(response.ValorLiquido, 0);
            Assert.Greater(response.ValorBruto, 0);

            double tolerance = 0.0001;

            Assert.That(response.ValorBruto, Is.EqualTo(1416.558486730710).Within(tolerance));
            Assert.That(response.ValorLiquido, Is.EqualTo(1354.0747137211038).Within(tolerance));
        }


        [Test]
        public void DeveRetornarValido()
        {
            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 12 };

            var result = _validator.TestValidate(request);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void DeveFalharSeValorInvestimentoZero()
        {
            var request = new RequestInvestimento() { ValorInvestimento = 0, PrazoMes = 12 };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(p => p.ValorInvestimento).WithErrorMessage("O valor de investimento deve ser maior que zero.");
        }

        [Test]
        public void DeveFalharSePrazoMesMenorOuIgualUm()
        {
            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 0 };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(p => p.PrazoMes).WithErrorMessage("O prazo mes deve ser maior que 1.");
        }
    }
}