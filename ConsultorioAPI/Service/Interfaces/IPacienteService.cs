using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IPacienteService
    {
        Task<List<Paciente>> GetAllPacientes();
        Task<Paciente> GetPacienteById(int Id);
        Task<List<Consulta>> GetPacienteConsultaById(int id);
        Task UpdatePaciente(PacienteDTO paciente, int Id);
        Task CreatePaciente(PacienteDTO paciente);
        Task<List<Paciente>> GetPacienteByIdade(int Idade);
        Task<List<Paciente>> GetPacienteByTipoSanguineo(string tipo);
        Task UpdateEnderecoPaciente(string endereco, int Id);
    }
}
