using ControleFinanceiro.Application.Conta;
using System.Net;
using System.Net.Mail;


namespace ControleFinanceiro.Application.EnvioEmail
{
    public class EnvioEmailService
    {
        private UsuarioService _usuarioService;
        private static Timer _timer;

        public EnvioEmailService(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public void EnvioEmailSchedulerTask()
        {
            var now = DateTime.Now;
            var proximaExecucao = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);

            if (now > proximaExecucao)
            {
                proximaExecucao = proximaExecucao.AddDays(1);
            }

            TimeSpan timeToGo = proximaExecucao - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            _timer = new Timer(x =>
            {
                EnviarEmail();
            }, null, timeToGo, TimeSpan.Zero);
        }

        public void EnviarEmail()
        {
            var list = _usuarioService.Listar();

            foreach (var item in list)
            {
                if (item.Conta.Saldo < 0)
                {

                    var smtpHost = "smtp.gmail.com";
                    var smtpPort = 587;
                    var smtpUsuario = "emailteste@gmail.com";
                    var smtpSenha = "abc123456";

                    using (var client = new SmtpClient(smtpHost, smtpPort))
                    {
                        client.Credentials = new NetworkCredential(smtpUsuario, smtpSenha);
                        client.EnableSsl = true;


                        var emailMessage = new MailMessage
                        {
                            From = new MailAddress(smtpUsuario),
                            Subject = "Controle Financeiro - Relatório Diário",
                            Body = "Seu saldo está negativo",
                            IsBodyHtml = true
                        };

                        emailMessage.To.Add(item.Email);

                        try
                        {
                            client.SendMailAsync(emailMessage);
                        }
                        catch (Exception ex)
                        {

                            throw new Exception($"Erro no envio de email - {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
