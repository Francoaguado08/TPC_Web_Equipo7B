using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Web
{
    public partial class AdministrarMarcas : System.Web.UI.Page
    {
        MarcasNegocio negocioMarca = new MarcasNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar las marcas en el GridView
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            // Obtener la lista de marcas de la lógica de negocio
            List<Marca> listaMarcas = negocioMarca.listar();

            // Asignar la lista al GridView
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de alta de marca
            Response.Redirect("AltaMarca.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Redirigir a la página principal de administración
            Response.Redirect("Administrar.aspx");
        }

        protected void gvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Poner la fila en modo edición
            gvMarcas.EditIndex = e.NewEditIndex;
            CargarMarcas();
        }

        protected void gvMarcas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancelar el modo edición
            gvMarcas.EditIndex = -1;
            CargarMarcas();
        }

        protected void gvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtener los valores editados
            int id = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);
            string nuevoNombre = ((TextBox)gvMarcas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            // Crear objeto Marca con los valores actualizados
            Marca marcaActualizada = new Marca
            {
                ID = id,
                Nombre = nuevoNombre
            };

            // Actualizar la marca en la base de datos
            negocioMarca.modificarMarca(marcaActualizada);

            // Salir del modo edición y recargar la tabla
            gvMarcas.EditIndex = -1;
            CargarMarcas();
        }

        protected void gvMarcas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID de la marca a eliminar
            int id = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);

            // Eliminar la marca de la base de datos
            negocioMarca.eliminarMarca(id);

            // Recargar la tabla
            CargarMarcas();
        }
    }
}
