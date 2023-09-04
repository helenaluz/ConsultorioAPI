namespace ConsultorioAPI.DTOs
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Nascimento { get; set; }
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string TipoSanguineo { get; set; } = string.Empty;
    }
}
