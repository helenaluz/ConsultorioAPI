using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _con;

        public EmailService(IConfiguration config, DataContext con) { _config = config; _con = con; }

		public async Task<string> ConfirmarEmail(string chave)
        {
            var token = await _con.Tokens.Where(t => t.Chave == chave).FirstOrDefaultAsync();
            if (token is null) return "Chave não encontrada!";

            var paciente = await _con.Pacientes.FirstOrDefaultAsync(p => p.Id == token.Id);
            if (paciente is null) return $"Paciente {token.Id} não encontrado!";

            paciente.EmailConfirmado = true;
            _con.Tokens.Remove(token);
            await _con.SaveChangesAsync();

            return "Email confirmado!";
        }

        public async Task<Paciente?> GetPacienteById(int id)
        {
            return await _con.Pacientes.FindAsync(id);
        }

        private async Task EnviarEmail(string tabela, int id, string emailDestino, string nomeDestino)
        {
            int minutos = 30;
            var configSection = _config.GetSection("EmailStrings");

            var token = new Token{
                Tabela = tabela,
                Id = id,
                Chave = Guid.NewGuid().ToString(),
                DataLimite = DateTime.Now.AddMinutes(minutos),
            };
            await _con.Tokens.AddAsync(token);
            await _con.SaveChangesAsync();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(configSection["UserName"], configSection["Address"]));
            message.To.Add(MailboxAddress.Parse(emailDestino));
            message.Subject = "Confirmação de acesso ao ConsultasAPI";
            message.Body = new TextPart(TextFormat.Html) { Text = $"{nomeDestino}, para acessar ao ConsultasAPI utilize a seguinte chave:<br>{token.Chave}<br>A confirmação deve ser feita em até {minutos} minutos." };

            using var smtp = new SmtpClient();
            smtp.Connect(configSection["Host"], int.Parse(configSection["Port"] ?? string.Empty), SecureSocketOptions.StartTls);
            try
            {
                smtp.Authenticate(configSection["Address"], configSection["Password"]);
                smtp.Send(message);
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }

        public async Task<string> PedirConfirmacao(Paciente paciente)
        {
            if (paciente.EmailConfirmado) return $"Email {paciente.Email} já confirmado!";
            if (paciente.Email == string.Empty) return $"Email não preenchido!";

            await EnviarEmail("Paciente", paciente.Id, paciente.Email, paciente.Nome);

            return $"Solicitação enviada para: {paciente.Email.Substring(0,2)}************{paciente.Email.Substring(paciente.Email.IndexOf('@')-1)}";
        }

		public async Task<string> GetConfirmouEmail(Paciente paciente)
        {
            if (paciente.EmailConfirmado) return "Email confirmado!";

            var token = await _con.Tokens.FirstOrDefaultAsync(t => t.Id == paciente.Id);
            if (token is null) return "Email não confirmado.";
            if (token.DataLimite >= DateTime.Now) return "Email não confirmado: Confirmação pendente.";
            return "Email não confirmado: Confirmação expirada.";
        }
    }
}