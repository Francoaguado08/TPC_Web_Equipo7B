using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener rol del usuario desde la sesión
                string rol = Session["Rol"] as string;

                // Encontrar elementos del menú por su ID
                var adminLink = FindControl("liAdmin") as HtmlGenericControl;
                var logoutLink = FindControl("btnLogout") as HtmlGenericControl;

                if (rol == "Admin")
                {
                    if (adminLink != null) adminLink.Visible = true; // Mostrar menú admin
                }
                else if (rol == "Cliente")
                {
                    if (adminLink != null) adminLink.Visible = false; // Ocultar menú admin
                }

                // Mostrar botón de cerrar sesión si está logueado
                if (logoutLink != null) logoutLink.Visible = !string.IsNullOrEmpty(rol);
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