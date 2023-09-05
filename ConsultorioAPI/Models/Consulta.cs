using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Prescricao { get; set; } = string.Empty;
        public bool Retorno { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
    }
}