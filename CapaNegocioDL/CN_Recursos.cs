using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; // OBIGATORIO
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Reflection;

namespace CapaNegocioDL
{
    public class CN_Recursos
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }


        public static string ConvertirSha256(string texto)
        {
            StringBuilder sB = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte b in result) 
                {
                    sB.Append(b.ToString("x2"));
                }

                return sB.ToString();
            }
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("sistemas2.comintec@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("sistemas2.comintec@gmail.com", "amcmwlcojkopqabn"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception)
            {
                resultado = true;

            }

            return resultado;
        }

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                conversion = false;               
            }
            return textoBase64;
        }
    }
}
