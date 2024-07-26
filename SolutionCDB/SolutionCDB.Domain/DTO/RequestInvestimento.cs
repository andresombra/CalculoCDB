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
        public double ValorInvestimento { get; set; }
        public int PrazoMes { get; set; }
    }
}
