namespace ConsultorioAPI.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public int AnoFormacao { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
    }
}