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

        public async Task<Consulta> CreateConsulta(ConsultaDTO consulta)
        {
            if (consulta == null) return null;

            Consulta nova = new Consulta {
                DataConsulta = consulta.DataConsulta,
                Descricao = consulta.Descricao,
                Prescricao = consulta.Prescricao,
                MedicoId = consulta.MedicoId,
                PacienteId = consulta.PacienteId
            };

            _con.Consultas.Add(nova);
            await _con.SaveChangesAsync();
            return nova;
        }
        public async Task<Consulta> UpdateConsulta(ConsultaDTO consulta, int Id)
        {
            if (consulta.Id != Id) return null;
            var existente = await _con.Consultas.FindAsync(Id);

            existente.DataConsulta = consulta.DataConsulta;
            existente.Descricao = consulta.Descricao;
            existente.Prescricao = consulta.Prescricao;
            existente.MedicoId = consulta.MedicoId;
            existente.PacienteId = consulta.PacienteId;
           
            await _con.SaveChangesAsync();
            return existente;
        }

        public async Task<Consulta> DeleteConsulta(int Id)
        {
            var consulta = await _con.Consultas.FindAsync(Id);
            if (consulta == null) return null;
             _con.Consultas.Remove(consulta);
            await _con.SaveChangesAsync();
            return consulta;
        }

        public async Task<List<Consulta>> GetAllConsultas()
        {
            return await _con.Consultas.ToListAsync();
        }

        public async Task<List<Consulta>> GetAllConsultasData(DateTime data)
        {
            data = data.Date;
            return await _con.Consultas.Where(c => c.DataConsulta.Date == data).ToListAsync();
        }
    }
}
