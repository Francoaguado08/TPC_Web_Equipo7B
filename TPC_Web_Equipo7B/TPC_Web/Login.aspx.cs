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
            if (Session["Usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Lógica de autenticación simulada (reemplazar con la lógica real)
            if (AutenticarUsuario(username, password))
            {
                // Almacena el nombre de usuario en la sesión y redirige a la página principal
                Session["Usuario"] = username;
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Muestra un mensaje de error si la autenticación falla
                lblError.Text = "Usuario o contraseña incorrectos.";
                lblError.Visible = true;
            }
        }

        private bool AutenticarUsuario(string username, string password)
        {
            // Reemplaza este código con tu lógica de autenticación real
            // Aquí podrías consultar una base de datos, por ejemplo
            return username == "admin" && password == "1234";
        }
    }
}
