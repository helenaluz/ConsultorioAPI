﻿using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IMedicoService
    {
        Task<List<Consulta>> GetAllConsultasMedicoById(int id);
        Task<List<Medico>> GetAllMedicos();
        Task<List<Medico>> GetAllMedicosByEspecialidade(string especialidade);
        Task<List<Medico>> GetAllMedicosDisponiveis(DateTime data, string especialidade);
        Task<Medico> GetMedicoByCrm(string crm);
        Task<Medico> CreateMedico(MedicoDTO request);
        Task<Medico> UpdateMedico(int id, MedicoDTOContato request);
        Task<Medico> UpdateEspecialidade(int id, string especialidade);
    }
}
