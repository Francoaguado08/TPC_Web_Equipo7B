using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarArticulos : System.Web.UI.Page
    {


        ArticuloNegocio negocioArticulo = new ArticuloNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los artículos en el GridView
                CargarArticulos();
            }
        }

        private void CargarArticulos()
        {
            // Obtener los artículos de la base de datos o la lista en sesión
            List<Articulo> listaArticulos = negocioArticulo.listar();

            // Asignar la lista al GridView
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }

        protected void gvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Verificar que hay una fila seleccionada
            if (gvArticulos.SelectedIndex >= 0)
            {
                // Obtener el ID del artículo seleccionado
                int id = Convert.ToInt32(gvArticulos.SelectedDataKey.Value);

                // Redirigir a la página de gestión de imágenes
                Response.Redirect("GestionarImagenes.aspx?id=" + id,false);
                // Detener el ciclo de vida de la página
                 Context.ApplicationInstance.CompleteRequest();
            }
        }
        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            // Redirige a la página de alta de artículo
            Response.Redirect("AltaProducto.aspx");
        }





    }
}