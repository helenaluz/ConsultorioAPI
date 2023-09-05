namespace ConsultorioAPI.Models
{
    public class Token
    {
        public string Tabela { get; set; } = string.Empty;
		public int Id { get; set; }
		public string Chave { get; set; } = string.Empty;
		public DateTime DataLimite { get; set; }
    }
}
