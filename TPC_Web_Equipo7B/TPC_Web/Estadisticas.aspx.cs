using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class Estadisticas : System.Web.UI.Page
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



            if (!IsPostBack)
            {
                CargarEstadisticas();
        
            }
            
            
        }



        private void CargarEstadisticas()
        {
            EstadisticasNegocio estadisticasNegocio = new EstadisticasNegocio();

            try
            {
                // Obtener estadísticas
                int cantidadUsuarios = estadisticasNegocio.ObtenerCantidadUsuarios();
                int cantidadCompras2024 = estadisticasNegocio.ObtenerCantidadCompras2024();

                //Mostrar resultados
                lblUsuariosRegistrados.Text = cantidadUsuarios.ToString();
                lblCompras2024.Text = cantidadCompras2024.ToString();
            }
            catch (Exception ex)
            {
                // Manejar errores
                lblUsuariosRegistrados.Text = "Error al cargar datos.";
                lblCompras2024.Text = "Error al cargar datos.";
               
            }
        }







    }
}