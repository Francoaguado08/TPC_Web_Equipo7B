using Dominio;
using Negocio;
using System;
using System.Web;
using System.Web.UI;

namespace TPC_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario ya ha iniciado sesión y lo redirige si es necesario
            if (Session["Usuarios"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Por favor ingrese un nombre de usuario y una contraseña.";
                lblError.Visible = true;
                return;
            }

            // Crear un objeto de usuario con los valores proporcionados
            Usuarios usuario = new Usuarios { Nombre = username, Contraseña = password };

            // Crear un objeto de la capa de negocio
            UsuarioNegocio negocio = new UsuarioNegocio();

            // Intentar loguearse
            if (negocio.Loguear(usuario))
            {
                // Redirigir a la página según el tipo de usuario
                if (usuario.TipoUsuario == TipoUsuario.Admin)
                {
                    // Redirigir a Administrar.aspx si es Admin
                    Response.Redirect("Administrar.aspx");
                }
                else
                {
                    // Redirigir a Default.aspx si es Cliente
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                // Si las credenciales no son correctas
                lblError.Text = "Nombre de usuario o contraseña incorrectos.";
                lblError.Visible = true;
            }
        }
    }
}
