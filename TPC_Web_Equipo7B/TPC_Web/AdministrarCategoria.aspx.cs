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
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            List<Categoria> listaCategorias = negocioCategoria.listar();
            gvCategorias.DataSource = listaCategorias;
            gvCategorias.DataBind();
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCategoria.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrar.aspx");
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;
            CargarCategorias();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
            string nuevoNombre = ((TextBox)gvCategorias.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            // Validar si el nombre ya existe
            if (negocioCategoria.ExisteNombreCategoria(nuevoNombre, id))
            {
                lblError.Text = "El nombre de la categoría ya existe. Por favor, elige otro nombre.";
                lblError.Visible = true;
                return;
            }

            Categoria categoriaActualizada = new Categoria
            {
                ID = id,
                Nombre = nuevoNombre
            };

            negocioCategoria.modificarCategoria(categoriaActualizada);
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }



        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
            negocioCategoria.eliminarCategoria(id);
            CargarCategorias();
        }
    }
}