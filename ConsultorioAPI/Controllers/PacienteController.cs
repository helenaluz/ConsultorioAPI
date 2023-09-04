using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        private readonly IPacienteService _service;
        public PacienteController(IPacienteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(PacienteDTO paciente)
        {
            if (paciente == null) return BadRequest("Preencha os campos!");
            var model = await _service.CreatePaciente(paciente);
            if (model is null) return BadRequest("Não foi possível criar o Paciente!");
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> GetAllPacientes(int? idade_maior_que)
        {
            if (idade_maior_que is null) return Ok(await _service.GetAllPacientes());

            if (idade_maior_que <= 0) return BadRequest("Idade precisa ser maior que 0!");
            return Ok(await _service.GetPacienteByIdade(idade_maior_que ?? 0));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Paciente>> GetPacienteById(int Id)
        {
            return Ok(await _service.GetPacienteById(Id));
        }

        [HttpGet("{Id}/consulta")]
        public async Task<ActionResult<List<Consulta>>> GetPacienteConsultaById(int Id)
        {
           return Ok(await  _service.GetPacienteConsultaById(Id));
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Paciente>> UpdatePaciente(int Id, [FromBody] string telefone)
        {
            var paciente = await _service.UpdatePaciente(Id, telefone);
            if (paciente is null) return NotFound($"Paciente {Id} não encontrado.");
            return Ok(paciente);
        }

        [HttpGet("sangue/{tipo}")]
        public async Task<ActionResult<List<Paciente>>> GetPacienteBySangue(string tipo)
        {
            return Ok(await _service.GetPacienteByTipoSanguineo(tipo));
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult<Paciente>> UpdateEnderecoPaciente([FromBody] string endereco, int Id)
        {
            var paciente = await _service.UpdateEnderecoPaciente(endereco, Id);
            if (paciente is null) return NotFound($"Paciente {Id} não encontrado.");
            return Ok(paciente);
        }
    }
}
