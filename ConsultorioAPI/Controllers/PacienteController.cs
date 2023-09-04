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

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> GetAllPacientes(int? idade_maior_que)
        {
            if (idade_maior_que is not null)
            {
                if (idade_maior_que <= 0) return BadRequest("Idade precisa ser maior que 0!");
                return Ok(await _service.GetPacienteByIdade(idade_maior_que ?? 0));
            }
            return Ok(await _service.GetAllPacientes());
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
        public async Task<ActionResult> UpdatePaciente(int Id, [FromBody] string telefone)
        {
    
            await _service.UpdatePaciente(Id, telefone);
            return Ok("Telefone atualizado com sucesso!");
        }

        [HttpPost]
        public async Task<ActionResult> CreatePaciente(PacienteDTO paciente)
        {
            if (paciente == null) return BadRequest("Preencha os campos!");
            await _service.CreatePaciente(paciente);
            return Ok(paciente.Nome + " criado com sucesso!");
        }

        [HttpGet("sangue/{tipo}")]
        public async Task<ActionResult<List<Paciente>>> GetPacienteBySangue(string tipo)
        {
            return Ok(await _service.GetPacienteByTipoSanguineo(tipo));
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateEnderecoPaciente(string endereco, int Id)
        {
           
            await _service.UpdateEnderecoPaciente(endereco, Id);
            return Ok(" Paciente atualizado com sucesso!");
        }
    }
}
