using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogin.Controllers
{
    public class EnvioEmail
    {
        public void EnviarEmail(string Nome, string Email, string Mensagem)
        {

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Sistema Login", "EMAIL"));//REMETENTE
            mailMessage.To.Add(new MailboxAddress(Nome, Email));//DESTINATARIO
            mailMessage.Subject = "Sistema Login";
            var builder = new BodyBuilder();
            builder.HtmlBody = string.Format("<html>" +
                        "<head> " +
                        "<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/> " +
                        "<title>Sistema Login</title> " +
                        "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>" +
                        "</head> " +
                        "<body style='background:#bababa; margin: 0; padding: 0;'>" +
                        "<table cellpadding='0' cellspacing='0' width='100%'> " +
                        "<tr>" +
                        "<td> " +
                        "<table align='center' cellpadding='0' cellspacing='0' width='600' style='background:#ffffff; border:1px #000000 solid;'>" +
                        "<tr> " +
                        "<td align='center' bgcolor='#fff' style='padding: 0px 0 20px 0;'>" +
                        "<br><img src='https://cdn.icon-icons.com/icons2/3178/PNG/512/diamond_computer_pc_sketch_jewelry_icon_193920.png' alt='Sistema Login' height='74' style='display: block;'/> " +
                        "</td>" +
                        "</tr> " +
                        "<tr>" +
                        "<td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'> " +
                        "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
                        "<tr> " +
                        "<td style='text-align: justify; color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>" +
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nome.ToLower()) + "," +
                        "</td>" +
                        "</tr> " +
                        "<tr>" +
                        "<td style='text-align: justify; padding: 10px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'> " +
                        "Tudo bem?" +
                        "<br><br> " +
                        Mensagem + "<br><br> " +
                        "Obrigado!" +
                        "<br><br> " +
                        "<br><br>" +
                        "</td> " +
                        "</tr>" +
                        "</table> " +
                        "<center><span style='text-align: justify; padding: 10px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height:20px;'>Este e-mail foi elaborado automaticamente via site do Sistema Login, por favor, não responder.</span></center> " +
                        "</td>" +
                        "</tr> " +
                        "<tr>" +
                        "<td bgcolor='#1861ac' style='text-align:center; padding: 30px 30px 30px 30px;'> " +
                        "<center><table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
                        "<tr> " +
                        "<td width='100%' style='text-align:center; color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;'>" +
                        "Todos direitos reservados © 2022 - Desenvolvido por Bruno Rodriguez " +
                        "</td>" +
                        "</tr> " +
                        "</table></center>" +
                        "</td> " +
                        "</tr>" +
                        "</table> " +
                        "</td>" +
                        "</tr> " +
                        "</table>" +
                        "</body> " +
                        "</html>"
                );
            mailMessage.Body = builder.ToMessageBody();


            using (var smtpClient = new SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
                smtpClient.Authenticate("EMAIL", "SENHA");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }


    }
}
