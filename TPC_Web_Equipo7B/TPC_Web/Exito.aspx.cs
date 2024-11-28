using System;
using System.Web;

namespace TPC_Web
{
    public partial class Exito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?; // 1 representa administrador

            if (idUsuario == null || tipoUsuario != 1)
            {
                // Si no está autenticado o no es administrador, redirige a la página de inicio
                Response.Redirect("Default.aspx", false);
                return; // Detiene la ejecución del método
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de administración de artículos
            Response.Redirect("AdministrarArticulos.aspx");
        }
    }
}
