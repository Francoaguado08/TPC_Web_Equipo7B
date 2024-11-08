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
            if (Session["Usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuarios usuarios;
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuarios = new Usuarios(0,txtUsername.Text,txtPassword.Text,false,"");
                if (negocio.Loguear(usuarios))
                {
                    Session.Add("Usuario",usuarios);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Session.Add("Error", "Usuario o contraseña incorrectas");
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
            }

        }
    }
}
