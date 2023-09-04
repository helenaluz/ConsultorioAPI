using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IConsultaService
    {
        Task CreateConsulta(ConsultaDTO consulta);
        Task UpdateConsulta(ConsultaDTO consulta, int Id);
        Task DeleteConsulta(int Id);
        Task<List<Consulta>> GetConsultaDataByMedicoId(int MedicoId, DateTime data);
    }
}
