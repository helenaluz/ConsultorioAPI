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

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<List<Consulta>>> GetAllConsultasMedicoById(int id)
        {
            var consultas = await _medicoService.GetAllConsultasMedicoById(id);
            return Ok(consultas);
        }

        [HttpGet]
        public async Task<ActionResult<List<Medico>>> GetAllMedicosByEspecialidade([FromQuery] string especialidade)
        {
            var medicos = await _medicoService.GetAllMedicosByEspecialidade(especialidade);
            return Ok(medicos);
        }

        [HttpGet("disponiveis")]
        public async Task<ActionResult<List<Medico>>> GetAllMedicosDisponiveis([FromQuery] DateTime data, [FromQuery] string especialidade)
        {
            var medicos = await _medicoService.GetAllMedicosDisponiveis(data, especialidade);
            return Ok(medicos);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CreateMedico(MedicoDTO request)
        {
            var medico = await _medicoService.CreateMedico(request);
            if (medico is null) return NotFound();

            return Ok(medico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medico>> UpdateMedico(int id, MedicoDTOContato request)
        {
            var medico = await _medicoService.UpdateMedico(id, request);
            if (medico is null) return NotFound();

            return Ok(medico);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Medico>> UpdateEspecialidade(int id, [FromBody] string especialidade)
        {
            var medico = await _medicoService.UpdateEspecialidade(id, especialidade);
            if (medico is null) return NotFound();

            return Ok(medico);
        }
    }
}
