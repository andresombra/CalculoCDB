using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCDB.Domain.DTO
{
    public class ResponseDto
    {
        public bool sucesso { get; set; }
        public string mensagem { get; set; }
        public object dados { get; set; }
    }
}
