using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _service;
        public EmailController(IEmailService service) { _service = service; }

        [HttpPut("confirmar/{chave}")]
        public async Task<ActionResult<string>> ConfirmarEmail(string chave)
        {
            return await _service.ConfirmarEmail(chave);
        }

        [HttpPost("solicitar/Paciente/{id}")]
        public async Task<ActionResult<string>> PedirConfirmacao(int id)
        {
            var paciente = await _service.GetPacienteById(id);
            if (paciente is null) return NotFound("Usuário não encontrado");

            return await _service.PedirConfirmacao(paciente);
        }

        [HttpGet("verificar/Paciente/{id}")]
        public async Task<ActionResult<string>> GetConfirmouEmail(int id)
        {
            var paciente = await _service.GetPacienteById(id);
            if (paciente is null) return NotFound("Usuário não encontrado");

            return await _service.GetConfirmouEmail(paciente);
        }
    }
}
