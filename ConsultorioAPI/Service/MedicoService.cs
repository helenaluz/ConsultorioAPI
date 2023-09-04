using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Service
{
    public class MedicoService : IMedicoService
    {
        private readonly DataContext _con;
        public MedicoService(DataContext con) { _con = con; }

        public async Task<List<Consulta>> GetAllConsultasMedicoById(int id)
        {
            return await _con.Consultas
                .Where(c => c.MedicoId == id)
                .ToListAsync();
        }

        public async Task<List<Medico>> GetAllMedicos()
        {
            return await _con.Medicos.ToListAsync();
        }

        public async Task<List<Medico>> GetAllMedicosByEspecialidade(string especialidade)
        {
            especialidade = especialidade.ToUpper();
            return await _con.Medicos
                .Where(m => m.Especialidade.ToUpper() == especialidade)
                .ToListAsync();
        }

        public async Task<List<Medico>> GetAllMedicosDisponiveis(DateTime data, string especialidade)
        {
            var dia = data.Date;
            var idMedicosConsulta = await _con.Consultas
                .Where(c => c.DataConsulta.Date == dia)
                .Select(c => c.MedicoId)
                .Distinct()
                .ToListAsync();

            especialidade = especialidade.ToUpper();
            return await _con.Medicos
                .Where(m => (m.Especialidade.ToUpper() == especialidade) && (!idMedicosConsulta.Contains(m.Id)))
                .ToListAsync();
        }

        public async Task<Medico> GetMedicoByCrm(string crm)
        {
            crm = crm.ToUpper();
            var medico = await _con.Medicos.FirstOrDefaultAsync(m => m.CRM.ToUpper() == crm);
            return medico;
        }

        public async Task<Medico> CreateMedico(MedicoDTO request)
        {
            var crmExistente = await _con.Medicos.FirstOrDefaultAsync(m => m.CRM.ToUpper() == request.CRM.ToUpper());
            if (crmExistente is not null) return null;

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
            await _con.Medicos.AddAsync(medico);
            await _con.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateMedico(int id, MedicoDTOContato request)
        {
            var medico = await _con.Medicos.FindAsync(id);
            if (medico is null) return null;

            medico.Telefone = request.Telefone;
            medico.Endereco = request.Endereco;

            await _con.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateEspecialidade(int id, string especialidade)
        {
            var medico = await _con.Medicos.FindAsync(id);
            if (medico is null) return null;

            medico.Especialidade = especialidade;

            await _con.SaveChangesAsync();
            return medico;
        }
    }
}
