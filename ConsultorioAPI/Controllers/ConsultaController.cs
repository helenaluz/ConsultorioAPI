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
        public async Task<ActionResult<Consulta>> CreateConsulta(ConsultaDTO consulta)
        {
            if (consulta == null) return BadRequest("Preencha os campos!");
            var model = await _service.CreateConsulta(consulta);
            if (model is null) return BadRequest("Não foi possível criar a Consulta!");
            return Ok(model);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Consulta>> UpdateConsulta(ConsultaDTO consulta, int Id)
        {
            if (consulta.Id != Id) return NotFound("IDs não combinam");
            var model = await _service.UpdateConsulta(consulta, Id);
            if (model is null) return NotFound($"Consulta {Id} não encontrada.");
            return Ok(model);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Consulta>> DeleteConsulta(int Id)
        {
            var model = await _service.DeleteConsulta(Id);
            if (model is null) return NotFound($"Consulta {Id} não encontrada.");
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<List<Consulta>>> GetAllConsultasData(DateTime? data)
        {
            if (data is null) return Ok(await _service.GetAllConsultas());

            return Ok(await _service.GetAllConsultasData(data ?? DateTime.Now));
        }
    }
}
