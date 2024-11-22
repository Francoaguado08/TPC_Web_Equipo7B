using Dominio;
using Negocio;
using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;

namespace TPC_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica si el usuario ya ha iniciado sesión y lo redirige si es necesario
            if (Session["Usuarios"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Por favor ingrese un correo electrónico y una contraseña.";
                lblError.Visible = true;
                return;
            }

            // Crear un objeto de usuario con los valores proporcionados
            Usuario usuario = new Usuario
            {
                Email = email,
                password = password
            };

            // Crear un objeto de la capa de negocio
            UsuarioNegocio negocio = new UsuarioNegocio();

            // Intentar loguearse
            if (negocio.Loguear(usuario))
            {
                // Guardar el usuario en la sesión
                Session["Usuario"] = usuario;

                // Redirigir según el tipo de usuario
                if (usuario.tipousuario == "Admin")
                {
                    Response.Redirect("Administrar.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                lblError.Text = "Correo electrónico o contraseña incorrectos.";
                lblError.Visible = true;
            }
        }
    }
}
