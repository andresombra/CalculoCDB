using Newtonsoft.Json;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using SolutionCDB.Service.Service;
using System.Drawing;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
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
        [TestCase(1000,6, 1059.7556770148981, 1046.310649686546)]
        [TestCase(1000,12, 1123.0820949653053, 1098.4656759722443)]
        [TestCase(1000,24, 1261.31339203165, 1215.5835484261183)]
        [TestCase(1000,36, 1416.558486730710, 1354.0747137211038)]
        public void CalcularCdbDadosBasicos(double valorInvestimento, int prazoMes, double valorBruto, double valorLiquido)
        {

            var request = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes = prazoMes };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);

            Assert.Greater(response.ValorLiquido, 0);
            Assert.Greater(response.ValorBruto, 0);

            double tolerance = 0.0001;

            Assert.AreEqual(valorBruto, response.ValorBruto, tolerance);
            Assert.AreEqual(valorLiquido, response.ValorLiquido, tolerance);
        }

        [Test]
        [TestCase(0,0, "O valor de investimento deve ser maior que zero.")]
        public async Task DeveRetornarMsgValidacaoValorInvestimento(double valorInvestimento, int prazoMes, string mensagemValidator)
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7096");

            var postData = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes= prazoMes };

            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync("/Cdb/Calcular", content).Result.Content.ReadFromJsonAsync<ResponseDto>();

            Assert.That(response.mensagem, Is.EqualTo(mensagemValidator));

        }

        [Test]
        [TestCase(1, 0, "O prazo mes deve ser maior que 1.")]
        public async Task DeveRetornarMsgValidacaoPrazoMes(double valorInvestimento, int prazoMes, string mensagemValidator)
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7096");

            var postData = new RequestInvestimento() { ValorInvestimento = valorInvestimento, PrazoMes = prazoMes };

            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync("/Cdb/Calcular", content).Result.Content.ReadFromJsonAsync<ResponseDto>();

            Assert.That(response.mensagem, Is.EqualTo(mensagemValidator));

        }


    }
}
