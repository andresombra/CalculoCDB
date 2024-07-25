using SolutionCDB.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Domain.Interfaces
{
    public interface ICdbService
    {
        Task<ResponseInvestimento> CalcularCdb(RequestInvestimento request);
    }
}
