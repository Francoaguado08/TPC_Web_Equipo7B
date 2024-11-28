using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarArticulos : System.Web.UI.Page
    {
        ArticuloNegocio negocioArticulo = new ArticuloNegocio();

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
                CargarArticulos();
            }
        }

        private void CargarArticulos()
        {
            List<Articulo> listaArticulos = negocioArticulo.listar();
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }

        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaProducto.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrar.aspx");
        }

        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgregarStock")
            {
                try
                {
                    int idArticulo = Convert.ToInt32(e.CommandArgument);

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    TextBox txtNuevoStock = (TextBox)row.FindControl("txtNuevoStock");

                    if (txtNuevoStock != null && int.TryParse(txtNuevoStock.Text, out int nuevoStock) && nuevoStock > 0)
                    {
                        negocioArticulo.AgregarStock(idArticulo, nuevoStock);
                        CargarArticulos();
                    }
                    else
                    {
                        Response.Write("<script>alert('Por favor, ingrese una cantidad válida.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }

        protected void gvArticulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Lógica para editar artículos
            gvArticulos.EditIndex = e.NewEditIndex;
            CargarArticulos();
        }

        protected void gvArticulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Actualizar los datos del artículo editado
            int idArticulo = Convert.ToInt32(gvArticulos.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvArticulos.Rows[e.RowIndex];
            string nuevoNombre = ((TextBox)row.FindControl("txtNombre")).Text;
            string nuevaDescripcion = ((TextBox)row.FindControl("txtDescripcion")).Text;

            Articulo articulo = new Articulo
            {
                ID = idArticulo,
                Nombre = nuevoNombre,
                Descripcion = nuevaDescripcion
            };

            negocioArticulo.modificar(articulo);

            gvArticulos.EditIndex = -1;
            CargarArticulos();
        }

        protected void gvArticulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvArticulos.EditIndex = -1;
            CargarArticulos();
        }

        protected void gvArticulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lógica para eliminar artículos
            int idArticulo = Convert.ToInt32(gvArticulos.DataKeys[e.RowIndex].Value);
            negocioArticulo.eliminarArticulo(idArticulo);
            CargarArticulos();
        }
    }
}
