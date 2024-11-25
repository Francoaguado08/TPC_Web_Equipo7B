using System;
using System.Web.UI;

namespace TPC_Web
{
    public partial class Administrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está logueado
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                // Redirigir al inicio si no está logueado o no tiene permiso de administrador
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