﻿using Microsoft.AspNetCore.Mvc;
using SolutionCDB.Domain.DTO;
using SolutionCDB.Domain.Interfaces;
using System.Net;

namespace SolutionCDB.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdbController : ControllerBase
    {
        private readonly ICdbService _cdbService;
        public CdbController(ICdbService cdbService)
        {
            _cdbService = cdbService;         
        }

        [HttpPost("Calcular")]
        public async Task<ResponseDto> CalcularCdb(RequestInvestimento request)
        {
            var resp = new ResponseDto();

            try
            {
                resp.dados = await _cdbService.CalcularCdb(request);
                resp.sucesso = resp.dados != null;
                return resp;
            }
            catch (Exception ex)
            {
                resp.sucesso = false;
                resp.mensagem = ex.Message.ToString();
                return resp;
            }
            
        }
    }
}