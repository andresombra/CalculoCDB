using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using SolutionCDB.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void CalcularCdbDadosBasicos()
        {

            var request = new RequestInvestimento() { ValorInvestimento=1000, PrazoMes=12 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);

        }

        [Test]
        public void TestaPrazoMesInvalido()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 1000, PrazoMes = 0 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);
            Assert.True(response.ValorLiquido.Equals(0));
            Assert.True(response.ValorBruto.Equals(0));

        }

        [Test]
        public void TestaValorInvestimentoPrazoMesComZero()
        {

            var request = new RequestInvestimento() { ValorInvestimento = 0, PrazoMes = 0 };

            var response = _cdbService.CalcularCdb(request).Result;

            Assert.IsNotNull(response);
            Assert.True(response.ValorLiquido.Equals(0));
            Assert.True(response.ValorBruto.Equals(0));
            
        }

        [Test]
        public async Task CalcularCdb_DeveRetornarValoresDefault_QuandoValorInvestimentoOuPrazoForInvalido()
        {
            // Arrange
            var request = new RequestInvestimento { ValorInvestimento = 0, PrazoMes = 12 };

            // Act
            var response = await _cdbService.CalcularCdb(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.That(response.ValorBruto, Is.EqualTo(0));
            Assert.That(response.ValorLiquido, Is.EqualTo(0));
        }

        [Test]
        public async Task CalcularCdb_DeveRetornarValoresCorretos_QuandoValorInvestimentoEPrazoForemValidos()
        {
            // Arrange
            var request = new RequestInvestimento { ValorInvestimento = 1000, PrazoMes = 12 };

            // Act
            var response = await _cdbService.CalcularCdb(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.ValorBruto > 1000);
            Assert.IsTrue(response.ValorLiquido < response.ValorBruto);
        }

        [Test]
        public async Task CalcularValorFinal_DeveRetornarValorEsperado_AposPrazoDe6Meses()
        {
            // Arrange
            double valorInvestimento = 1000;
            int prazoMeses = 6;

            // Act
            double valorFinal = await _cdbService.CalcularValorFinalAsync(valorInvestimento, prazoMeses);

            // Assert
            Assert.That(valorFinal, Is.EqualTo(1059.75).Within(0.01));  // Substitua o valor esperado por um cálculo real
        }

        [Test]
        public async Task CalcularValorLiquido_DeveAplicarTaxaCorretaComPrazoDe12Meses()
        {
            // Arrange
            double valorInvestimento = 1000;
            double valorFinal = 1080;  // Supondo valor bruto após 12 meses
            int prazoMeses = 12;

            // Act
            double valorLiquido = await _cdbService.CalcularValorLiquidoAsync(valorInvestimento, valorFinal, prazoMeses);

            // Assert
            Assert.That(valorLiquido, Is.EqualTo(1064).Within(0.01));  // Substitua o valor esperado por um cálculo real
        }

        [Test]
        public async Task RetornaTaxaImposto_DeveRetornarTaxaCorretaComPrazoDe24Meses()
        {
            // Arrange
            int prazoMeses = 24;

            // Act
            double taxaImposto = await _cdbService.RetornaTaxaImpostoAsync(prazoMeses);

            // Assert
            Assert.That(taxaImposto, Is.EqualTo(0.175));
        }
    }
}
