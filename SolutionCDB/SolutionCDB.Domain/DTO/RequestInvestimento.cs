using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Domain.DTO
{
    public class RequestInvestimento
    {

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do investimento deve ser maior que zero.")]
        public double ValorInvestimento { get; set; }
        public int PrazoMes { get; set; }
    }
}
