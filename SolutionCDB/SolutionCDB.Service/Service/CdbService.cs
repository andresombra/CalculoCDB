using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Service.Service
{
    public class CdbService : ICdbService
    {
        private const double TaxaBase = 1.08;
        private const double Cdi = 0.009;
        public async Task<ResponseInvestimento> CalcularCdb(RequestInvestimento request)
        {
            if (request.ValorInvestimento <= 0 || request.PrazoMes <= 0) return new ResponseInvestimento();

            double valorResultado = await CalcularValorFinalAsync(request.ValorInvestimento, request.PrazoMes);
            double valorLiquido = await CalcularValorLiquidoAsync(request.ValorInvestimento, valorResultado, request.PrazoMes);

            return new ResponseInvestimento() { ValorBruto = valorResultado, ValorLiquido = valorLiquido };
        }
        private async Task<double> CalcularValorFinalAsync(double valorInvestimento, int prazoMeses)
        {
            double valorFinal = valorInvestimento;
            for (int i = 0; i < prazoMeses; i++)
            {
                valorFinal *= (1 + (Cdi * TaxaBase));
            }
            return await Task.FromResult(valorFinal);
        }

        private async Task<double> CalcularValorLiquidoAsync(double valorInvestimento, double valorResultado, int prazoMeses)
        {
            double taxaImposto = await RetornaTaxaImpostoAsync(prazoMeses);
            return valorResultado - (valorResultado - valorInvestimento) * taxaImposto;
        }

        private async Task<double> RetornaTaxaImpostoAsync(int prazoMeses)
        {
            return await Task.FromResult(prazoMeses) switch
            {
                <= 6 => 0.225,
                <= 12 => 0.2,
                <= 24 => 0.175,
                _ => 0.15,
            };
        }
    }
}
