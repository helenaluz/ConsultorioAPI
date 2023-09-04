using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IConsultaService
    {
        Task<Consulta> CreateConsulta(ConsultaDTO consulta);
        Task<Consulta> UpdateConsulta(ConsultaDTO consulta, int Id);
        Task<Consulta> DeleteConsulta(int Id);
        Task<List<Consulta>> GetAllConsultas();
        Task<List<Consulta>> GetAllConsultasData(DateTime data);
    }
}
