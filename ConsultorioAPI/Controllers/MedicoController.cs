using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {

        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CreateMedico(MedicoDTO request)
        {
            if (request is null) return BadRequest("Preencha os campos!");
            var medico = await _medicoService.CreateMedico(request);
            if (medico is null) return BadRequest("Não foi possível criar o Médico!");

            return Ok(medico);
        }

        [HttpGet]
        public async Task<ActionResult<List<Medico>>> GetAllMedicosByEspecialidade([FromQuery] string? especialidade)
        {
            if (especialidade is null) return Ok(await _medicoService.GetAllMedicos());

            return Ok(await _medicoService.GetAllMedicosByEspecialidade(especialidade));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medico>> UpdateMedico(int id, MedicoDTOContato request)
        {
            if (request is null) return BadRequest("Preencha os campos!");
            var medico = await _medicoService.UpdateMedico(id, request);
            if (medico is null) return NotFound($"Médico {id} não encontrado.");

            return Ok(medico);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Medico>> UpdateEspecialidade(int id, [FromBody] string especialidade)
        {
            var medico = await _medicoService.UpdateEspecialidade(id, especialidade);
            if (medico is null) return NotFound($"Médico {id} não encontrado.");

            return Ok(medico);
        }

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<List<Consulta>>> GetAllConsultasMedicoById(int id)
        {
            var consultas = await _medicoService.GetAllConsultasMedicoById(id);
            return Ok(consultas);
        }

        [HttpGet("disponiveis")]
        public async Task<ActionResult<List<Medico>>> GetAllMedicosDisponiveis([FromQuery] DateTime data, [FromQuery] string especialidade)
        {
            var medicos = await _medicoService.GetAllMedicosDisponiveis(data, especialidade);
            return Ok(medicos);
        }

        [HttpGet("crm/{crm}")]
        public async Task<ActionResult<Medico>> GetAllMedicosByFormacao(string crm)
        {
            var medico = await _medicoService.GetMedicoByCrm(crm);
            if (medico is null) return NotFound($"Médico de CRM '{crm}' não encontrado.");

            return Ok(medico);
        }
    }
}
