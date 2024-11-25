using Dominio;
using Negocio;
using System;
using System.Web.UI;

namespace TPC_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario ya ha iniciado sesión y lo redirige si es necesario
            if (Session["Usuario"] != null)
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
                lblError.Text = "Por favor, ingrese un correo electrónico y una contraseña.";
                lblError.Visible = true;
                return;
            }

            try
            {
                // Crear una instancia de la capa de negocio
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Autenticar al usuario según su email y contraseña
                Usuario usuario = negocio.autenticar(email, password);

                if (usuario != null)
                {
                    // Guardar información relevante del usuario en la sesión
                    Session["IDUsuario"] = usuario.IDUsuario;
                    Session["tipoUsuario"] = usuario.tipousuario; // "1" para Admin, "2" para Cliente
                    Session["Email"] = usuario.Email; // Agregar esta línea

                    // Redirigir según el tipo de usuario
                    if (usuario.tipousuario == 1)
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
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al procesar su solicitud. Intente nuevamente.";
                lblError.Visible = true;

                // Opcional: registrar el error en un archivo o sistema de logs
                System.Diagnostics.Debug.WriteLine($"Error en Login: {ex.Message}");
            }
        }



        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario.aspx");
        }


    }
}
