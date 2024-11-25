using Dominio;
using Negocio;
using System;
using System.Web.UI;

namespace TPC_Web
{
    public partial class VerificarUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sin lógica inicial
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Por favor, complete todos los campos.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            if (negocio.EmailExiste(email))
            {
                lblMensaje.Text = "El correo ya está registrado.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                btnRegistrar.Visible = false;
            }
            else
            {
                lblMensaje.Text = "El correo es válido. Puede registrarse.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
                btnRegistrar.Visible = true; // Mostrar botón de registro
            }

            // Reasignar el valor del password al TextBox
            txtPassword.Attributes["value"] = password;
        }


        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            Usuario nuevoUsuario = new Usuario
            {
                Email = email,
                password = password,
                tipousuario = (int)Usuario.TipoUsuario.Cliente // Asignar 2 como valor entero
            };

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.agregar(nuevoUsuario);

            // Obtener el ID generado para el nuevo usuario
            int nuevoId = negocio.ObtenerIdPorEmail(email);

            // Redirigir a la página DatosPersonales.aspx con el ID como parámetro
            Response.Redirect($"DatosPersonales.aspx?IDUsuario={nuevoId}");
        }

    }
}
