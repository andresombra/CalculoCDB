using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using System;
using System.Net;

namespace SolutionCDB.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdbController : ControllerBase
    {
        private readonly ICDBService _cdbService;
        private readonly IValidator<RequestInvestimento> _validator;
        public CdbController(ICDBService cdbService, IValidator<RequestInvestimento> validator)
        {
            _cdbService = cdbService;         
            _validator = validator;
        }

        [HttpPost("Calcular")]
        public async Task<IActionResult> CalcularCdb(RequestInvestimento request)
        {
            var resp = new ResponseDto();

            try
            {
                ValidationResult result = await _validator.ValidateAsync(request);

                if (!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);
                    return BadRequest(ModelState);
                }

                resp.dados = await _cdbService.CalcularCdb(request);
                resp.sucesso = resp.dados != null;

                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.sucesso = false;
                resp.mensagem = ex.Message.ToString();

                return BadRequest(resp);
            }
            
        }
    }
}
