using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using SolutionCDB.Service.Service;
namespace SolutionCDB.Tests
{
    [TestFixture]
    public class TestePrazoMeses
    {
        CdbService _cdbService;

        [SetUp]
        public void Setup()
        {
            _cdbService = new CdbService();
        }

        [Test]
        [TestCase(1000,6)]
        [TestCase(1000,12)]
        [TestCase(1000,24)]
        [TestCase(1000,36)]
        public void CalcularCdbDadosBasicos(double valorInvestimento, int prazoMes)
        {

            var request = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes = prazoMes };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);

        }

    }
}
