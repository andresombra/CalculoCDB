using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Domain.DTO
{
    public class RequestInvestimento
    {
        public decimal Valor { get; set; }
        public int PrazoMes { get; set; }
    }
}
