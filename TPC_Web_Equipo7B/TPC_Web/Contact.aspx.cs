using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;



namespace TPC_Web
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturar datos del formulario
                string nombre = txtNombre.Text.Trim();
                string email = txtEmail.Text.Trim();
                string mensaje = txtMensaje.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mensaje))
                {
                    lblResultado.Text = "Por favor, complete todos los campos.";
                    lblResultado.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Configuración del mensaje de correo
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("e7b.contacto@gmail.com"); //
                mail.To.Add("e7b.contacto@gmail.com"); // 
                mail.Subject = "Nuevo mensaje de contacto";
                mail.Body = $"Nombre: {nombre}\nEmail: {email}\nMensaje:\n{mensaje}";

                // Configuración del cliente SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new NetworkCredential("e7b.contacto@gmail.com", "ksmh efll zarz wdrk"); // Aquí usa la contraseña de aplicación
                smtp.EnableSsl = true; // Habilitar SSL

                // Enviar correo
                smtp.Send(mail);

                lblResultado.Text = "¡Mensaje enviado con éxito!";
                lblResultado.ForeColor = System.Drawing.Color.Green;

                // Limpiar campos del formulario
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtMensaje.Text = "";
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error al enviar el mensaje: " + ex.Message;
                lblResultado.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
