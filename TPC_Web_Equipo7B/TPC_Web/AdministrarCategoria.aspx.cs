using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

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

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de alta de categoría
            Response.Redirect("AltaCategoria.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Redirigir a la página principal de administración
            Response.Redirect("Administrar.aspx");
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Poner la fila en modo edición
            gvCategorias.EditIndex = e.NewEditIndex;
            CargarCategorias();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancelar el modo edición
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtener los valores editados
            int id = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
            string nuevoNombre = ((TextBox)gvCategorias.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            // Crear objeto Categoría con los valores actualizados
            Categoria categoriaActualizada = new Categoria
            {
                ID = id,
                Nombre = nuevoNombre
            };

            // Actualizar la categoría en la base de datos
            negocioCategoria.modificarCategoria(categoriaActualizada);

            // Salir del modo edición y recargar la tabla
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID de la categoría a eliminar
            int id = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

            // Eliminar la categoría de la base de datos
            negocioCategoria.eliminarCategoria(id);

            // Recargar la tabla
            CargarCategorias();
        }
    }
}
