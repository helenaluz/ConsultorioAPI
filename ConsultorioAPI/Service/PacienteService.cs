using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConsultorioAPI.Service
{
    public class PacienteService  : IPacienteService
    {
        /*GetAllConsultasPacienteById : GET !
UpdatePaciente : PUT !
CreatePaciente : POST (DTO) !
GetPacienteIdade : GET !
UpdateEndereco : PATCH !
GetPacientesBySangue : GET !*/

        private readonly DataContext _con;
        public PacienteService(DataContext con) { _con = con; }

        public async Task<List<Consulta>> GetPacienteConsultaById(int id) {  return await _con.Consultas.Where(c => c.PacienteId == id).ToListAsync(); }

        public async Task UpdatePaciente(PacienteDTO paciente, int Id)
        {
            if (Id != paciente.Id) return;
            var existente = await _con.Pacientes.FindAsync(Id);

            existente.Nome = paciente.Nome;
            existente.Nascimento = paciente.Nascimento;
            existente.CPF = paciente.CPF;
            existente.Telefone = paciente.Telefone;
            existente.Endereco = paciente.Endereco;
            existente.Sexo = paciente.Sexo;
            existente.TipoSanguineo = paciente.TipoSanguineo;

            await _con.SaveChangesAsync();
        }

        public async Task CreatePaciente(PacienteDTO paciente)
        {
            Paciente novo = new Paciente
            {
                Nome = paciente.Nome,
                Nascimento = paciente.Nascimento,
                CPF = paciente.CPF,
                Telefone = paciente.Telefone,
                Endereco = paciente.Endereco,
                Sexo = paciente.Sexo,
                TipoSanguineo = paciente.TipoSanguineo
            };
            _con.Pacientes.Add(novo);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Paciente>> GetPacienteByIdade(int Idade) { 
            DateTime AnoNascimento = DateTime.Now.AddYears(-Idade).Date;

            return await _con.Pacientes.Where(p => p.Nascimento.Date <= AnoNascimento).ToListAsync(); 
        }
        public async Task<List<Paciente>> GetPacienteByTipoSanguineo(string tipo) { return await _con.Pacientes.Where(p => p.TipoSanguineo == tipo).ToListAsync(); }
        public async Task UpdateEnderecoPaciente(string endereco, int Id)
        {
            var paciente = _con.Pacientes.FirstOrDefault(p => p.Id == Id);
            if (paciente == null) return ;
            paciente.Endereco = endereco;
            await _con.SaveChangesAsync();
        }

        public async Task<List<Paciente>> GetAllPacientes()
        {
            return await _con.Pacientes.ToListAsync();
        }

        public async Task<Paciente> GetPacienteById(int Id)
        {
            return await _con.Pacientes.FindAsync(Id);
        }
    }
}
