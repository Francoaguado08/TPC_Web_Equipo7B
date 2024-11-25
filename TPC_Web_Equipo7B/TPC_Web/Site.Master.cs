using System;
using System.Web.UI;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario está logueado
                int? idUsuario = Session["IDUsuario"] as int?;
                int? tipoUsuario = Session["tipoUsuario"] as int?;

                // Configurar visibilidad de los botones según el estado de la sesión
                liLogin.Visible = idUsuario == null; // Mostrar Login si no hay sesión
                liPerfil.Visible = idUsuario != null; // Mostrar Perfil si el usuario está logueado
                btnLogout.Visible = idUsuario != null; // Mostrar Cerrar Sesión si el usuario está logueado
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Redirigir al login
            Response.Redirect("Login.aspx");
        }
    }
}