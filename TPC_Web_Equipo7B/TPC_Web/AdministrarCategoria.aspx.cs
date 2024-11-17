using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarCategorias : System.Web.UI.Page
    {
        CategoriaNegocio negocioCategoria = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar las categorías en el GridView
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            // Obtener la lista de categorías de la lógica de negocio
            List<Categoria> listaCategorias = negocioCategoria.listar();

            // Asignar la lista al GridView
            gvCategorias.DataSource = listaCategorias;
            gvCategorias.DataBind();
        }

        protected void gvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvCategorias.SelectedIndex >= 0)
            {
                // Obtener el ID de la categoría seleccionada
                int id = Convert.ToInt32(gvCategorias.SelectedDataKey.Value);

                // Redirigir a la página de edición de la categoría
                Response.Redirect("GestionarCategoria.aspx?id=" + id, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de alta de categoría
            Response.Redirect("AltaCategoria.aspx");
        }
    }
}
