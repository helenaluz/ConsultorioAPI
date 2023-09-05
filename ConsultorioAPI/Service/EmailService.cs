using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
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

        private async Task EnviarEmail(string tabela, int id, string emailDestino, string nomeDestino, string url)
        {
            int minutos = 30;
            var configSection = _config.GetSection("EmailStrings");

            var token = await _con.Tokens.FindAsync(tabela, id);
            if (token is null)
            {
                token = new Token{
                    Tabela = tabela,
                    Id = id,
                    Chave = Guid.NewGuid().ToString(),
                    DataLimite = DateTime.Now.AddMinutes(minutos),
                };
                await _con.Tokens.AddAsync(token);
            }
            else
            {
                token.Chave = Guid.NewGuid().ToString();
                token.DataLimite = DateTime.Now.AddMinutes(minutos);
            }
            await _con.SaveChangesAsync();

            url = url.Replace("{chave}", token.Chave);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(configSection["UserName"], configSection["Address"]));
            message.To.Add(MailboxAddress.Parse(emailDestino));
            message.Subject = "Confirmação de e-mail";
            message.Body = new TextPart(TextFormat.Html) { Text = $"{nomeDestino},<br>Para confirmar o seu e-mail junto ao sistema ConsultorioAPI <a href={url}>clique aqui</a> ou acesse o link abaixo:<br><br>{url}<br><br>O link expira em {minutos} minutos, após isto será necessário solicitar uma nova confirmação." };

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

        private string FiltraEmail(string email)
        {
            return $"{email.Substring(0,2)}************{email.Substring(email.IndexOf('@')-1)}";
        }

        public async Task<string> PedirConfirmacao(Paciente paciente, string url)
        {
            if (paciente.EmailConfirmado) return $"Email já confirmado! ({FiltraEmail(paciente.Email)})";
            if (paciente.Email == string.Empty) return $"Email não preenchido!";

            await EnviarEmail("Paciente", paciente.Id, paciente.Email, paciente.Nome, url);

            return $"Solicitação enviada para: {FiltraEmail(paciente.Email)}";
        }

		public async Task<string> GetConfirmouEmail(Paciente paciente)
        {
            if (paciente.EmailConfirmado) return "Email confirmado!";

            var token = await _con.Tokens.FirstOrDefaultAsync(t => t.Id == paciente.Id);
            if (token is null) return "Email não confirmado.";
            if (token.DataLimite >= DateTime.Now) return "Email não confirmado: Solicitação pendente.";
            return "Email não confirmado: Solicitação expirou.";
        }
    }
}