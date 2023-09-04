using ConsultorioAPI.Models;
using ConsultorioAPI.DTOs;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IMedicoService
    {
        Task<List<Consulta>> GetAllConsultasMedicoById(int id);
        Task<List<Medico>> GetAllMedicosByEspecialidade(string especialidade);
        Task<List<Medico>> GetAllMedicosDisponiveis(DateTime data, string especialidade);
        Task<Medico> CreateMedico(MedicoDTO request);
        Task<Medico> UpdateMedico(int id, MedicoDTOContato request);
        Task<Medico> UpdateEspecialidade(int id, string especialidade);
    }
}
