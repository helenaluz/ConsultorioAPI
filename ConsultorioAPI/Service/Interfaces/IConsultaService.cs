using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IConsultaService
    {
        Task CreateConsulta(ConsultaDTO consulta);
        Task UpdateConsulta(ConsultaDTO consulta, int Id);
        Task DeleteConsulta(int Id);
        Task<List<Consulta>> GetAllConsultas();
        Task<List<Consulta>> GetAllConsultasData(DateTime data);
    }
}
