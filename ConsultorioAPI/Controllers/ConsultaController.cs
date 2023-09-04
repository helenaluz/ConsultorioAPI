using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _service;
        public ConsultaController(IConsultaService service) { _service = service; }

        [HttpPost]
        public async Task<ActionResult> CreateConsulta(ConsultaDTO consulta)
        {
            if (consulta == null) { return BadRequest("Preencha os campos!"); }
            await _service.CreateConsulta(consulta);
            return Ok(consulta);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateConsulta(ConsultaDTO consulta, int Id)
        {
            if (consulta.Id != Id) return NotFound("IDs não combinam");
            await _service.UpdateConsulta(consulta, Id);
            return Ok("Consulta atualizada com sucesso!");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteConsulta(int Id)
        {
            await _service.DeleteConsulta(Id);
            return Ok("Consulta deleetada com sucesso!");
        }

        [HttpGet("{MedicoId}")]
        public async Task<ActionResult<List<Consulta>>> GetConsultaDataByMedicoId(int MedicoId, DateTime data)
        {
            return await _service.GetConsultaDataByMedicoId(MedicoId, data);
        }
    }
}
