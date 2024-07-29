using SolutionCDB.Domain.DTO;
using SolutionCDB.Service.Service;

namespace SolutionCDB.Tests
{
    [TestFixture]
    public class TestarCalculo
    {
        CdbService _cdbService;

        [SetUp]
        public void Setup()
        {
            _cdbService = new CdbService();
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(5000, 0)]
        [TestCase(0, 1)]
        public void ValidarDadosRequest(double valorInvestimento, int prazoMes)
        {

            var request = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes = prazoMes };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);
            Assert.True(response.ValorLiquido.Equals(0));
            Assert.True(response.ValorBruto.Equals(0));

        }

        [Test]
        public void CalcularCdbDadosBasicos()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 12 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);
            Assert.True(response.ValorLiquido > 0);
            Assert.True(response.ValorBruto > 0);


        }

        [Test]
        public void TestaPrazoMesInvalido()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 0 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.True(response.ValorLiquido.Equals(0));
            Assert.True(response.ValorBruto.Equals(0));

        }

        [Test]
        public void TestaValorInvestimentoPrazoMesComZero()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 0, PrazoMes = 0 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.True(response.ValorLiquido.Equals(0));
            Assert.True(response.ValorBruto.Equals(0));

        }

        [Test]
        public void TestarCalculoVrInvestimento5000Prazomes24()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 5000, PrazoMes = 24 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.True(response.ValorLiquido.Equals(6077.917742130592));
            Assert.True(response.ValorBruto.Equals(6306.566960158294));

        }

    }
}
