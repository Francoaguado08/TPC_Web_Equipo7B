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
                int rol = Session["tipoUsuario"] != null ? Convert.ToInt32(Session["tipoUsuario"]) : 0;

                // Depuración: Imprimir en el log el valor del rol
                System.Diagnostics.Debug.WriteLine($"Rol actual: {rol}");

                // Encontrar elementos del menú por su ID
                var adminLink = FindControl("liAdmin") as HtmlGenericControl;
                var logoutLink = FindControl("btnLogout") as HtmlGenericControl;
                var loginLink = FindControl("liLogin") as HtmlGenericControl;

                if (adminLink == null || logoutLink == null || loginLink == null)
                {
                    System.Diagnostics.Debug.WriteLine("No se encontraron algunos elementos en el DOM.");
                }

                // Configurar visibilidad de elementos según el rol
                if (rol == 1) // Admin
                {
                    if (adminLink != null) adminLink.Visible = true; // Mostrar menú admin
                }
                else if (rol == 2) // Cliente
                {
                    if (adminLink != null) adminLink.Visible = false; // Ocultar menú admin
                }

                // Mostrar botón de cerrar sesión si está logueado
                if (logoutLink != null) logoutLink.Visible = rol != 0;

                // Ocultar botón de login si está logueado
                if (loginLink != null) loginLink.Visible = rol == 0;
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
