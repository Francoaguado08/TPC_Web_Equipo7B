using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_Web
{
    public partial class AdministrarMarcas : System.Web.UI.Page
    {
        MarcasNegocio negocioMarca = new MarcasNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar las marcas al cargar la página por primera vez
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            // Obtener la lista de marcas desde la lógica de negocio
            List<Marca> listaMarcas = negocioMarca.listar();

            // Asignar la lista al GridView
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }

        protected void gvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvMarcas.SelectedIndex >= 0)
            {
                // Obtener el ID de la marca seleccionada
                int id = Convert.ToInt32(gvMarcas.SelectedDataKey.Value);

                // Redirigir a la página de gestión de la marca
                Response.Redirect("GestionarMarca.aspx?id=" + id, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            // Redirigir a la página para agregar una nueva marca
            Response.Redirect("AltaMarca.aspx");
        }
    }
}
