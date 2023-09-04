using Microsoft.EntityFrameworkCore;
using ConsultorioAPI.Service.Interfaces;
using ConsultorioAPI.Models;
using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs;

namespace ConsultorioAPI.Service
{
    public class MedicoService : IMedicoService
    {
        private readonly DataContext _dataContext;

        public MedicoService(DataContext dataContext) { _dataContext = dataContext; }

        public async Task<List<Consulta>> GetAllConsultasMedicoById(int id)
        {
            return await _dataContext.Consultas
                .Where(c => c.MedicoId == id)
                .ToListAsync();
        }

        public async Task<List<Medico>> GetAllMedicosByEspecialidade(string especialidade)
        {
            especialidade = especialidade.ToUpper();
            return await _dataContext.Medicos
                .Where(m => m.Especialidade.ToUpper() == especialidade)
                .ToListAsync();
        }

        public async Task<List<Medico>> GetAllMedicosDisponiveis(DateTime data, string especialidade)
        {
            var dia = data.Date;
            var idMedicosConsulta = await _dataContext.Consultas
                .Where(c => c.DataConsulta.Date == dia)
                .Select(c => c.MedicoId)
                .Distinct()
                .ToListAsync();

            especialidade = especialidade.ToUpper();
            return await _dataContext.Medicos
                .Where(m => (m.Especialidade.ToUpper() == especialidade) && (!idMedicosConsulta.Contains(m.Id)))
                .ToListAsync();
        }

        public async Task<Medico> CreateMedico(MedicoDTO request)
        {
            Medico medico = new Medico{
                Nome = request.Nome,
                CRM = request.CRM,
                Especialidade = request.Especialidade,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                DataNascimento = request.DataNascimento,
                Sexo = request.Sexo,
                AnoFormacao = request.AnoFormacao,
            };
            await _dataContext.Medicos.AddAsync(medico);
            await _dataContext.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateMedico(int id, MedicoDTOContato request)
        {
            var medico = await _dataContext.Medicos.FindAsync(id);
            if (medico is null) return null;

            medico.Telefone = request.Telefone;
            medico.Endereco = request.Endereco;

            await _dataContext.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateEspecialidade(int id, string especialidade)
        {
            var medico = await _dataContext.Medicos.FindAsync(id);
            if (medico is null) return null;

            medico.Especialidade = especialidade;

            await _dataContext.SaveChangesAsync();
            return medico;
        }
    }
}
