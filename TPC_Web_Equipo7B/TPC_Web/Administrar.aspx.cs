using System;
using System.Web.UI;

namespace TPC_Web
{
    public partial class Administrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] == null || (string)Session["Rol"] != "Admin")
            {
                // Redirigir al inicio si no es admin
                Response.Redirect("Default.aspx");
            }






        }

        protected void btnAdministrarArticulos_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarArticulos.aspx");
        }

        protected void btnAdministrarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarCategoria.aspx");
        }

        protected void btnAdministrarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarMarca.aspx");
        }

        protected void btnAdministrarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarUsuario.aspx");
        }
    }
}
