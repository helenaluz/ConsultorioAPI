using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Immutable;
/*
CreateConsulta : POST !
DeleteConsulta : DELETE !
GetConsultaData : GET !
UpdateConsulta : PUT
*/

namespace ConsultorioAPI.Service
{
    public class ConsultaService : IConsultaService
    {
        private readonly DataContext _con;
        public ConsultaService(DataContext con) { _con = con; }

        public async Task CreateConsulta(ConsultaDTO consulta)
        {
            if (consulta == null) return;

            Consulta nova = new Consulta {
                DataConsulta = consulta.DataConsulta,
                Descricao = consulta.Descricao,
                Prescricao = consulta.Prescricao,
                MedicoId = consulta.MedicoId,
                PacienteId = consulta.PacienteId
            };

            _con.Consultas.Add(nova);
            await _con.SaveChangesAsync();
        }
        public async Task UpdateConsulta(ConsultaDTO consulta, int Id)
        {
            if (consulta.Id != Id) return;
            var existente = await _con.Consultas.FindAsync(Id);

            existente.DataConsulta = consulta.DataConsulta;
            existente.Descricao = consulta.Descricao;
            existente.Prescricao = consulta.Prescricao;
            existente.MedicoId = consulta.MedicoId;
            existente.PacienteId = consulta.PacienteId;
           
            await _con.SaveChangesAsync();
        }

        public async Task DeleteConsulta(int Id)
        {
            var consulta = await _con.Consultas.FindAsync(Id);
            if (consulta == null) return;
             _con.Consultas.Remove(consulta);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Consulta>> GetAllConsultasData(DateTime data)
        {
            data = data.Date;
            return await _con.Consultas.Where(c => c.DataConsulta.Date == data).ToListAsync();
        }
    }
}
