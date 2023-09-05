using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IEmailService
    {
        Task<string> ConfirmarEmail(string chave);

        Task<Paciente?> GetPacienteById(int id);
        Task<string> PedirConfirmacao(Paciente paciente, string url);
        Task<string> GetConfirmouEmail(Paciente paciente);
    }
}