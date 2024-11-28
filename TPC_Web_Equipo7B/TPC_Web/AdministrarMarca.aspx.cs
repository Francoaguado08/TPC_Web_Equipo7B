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
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            List<Marca> listaMarcas = negocioMarca.listar();
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMarca.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrar.aspx");
        }

        protected void gvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMarcas.EditIndex = e.NewEditIndex;
            CargarMarcas();
        }

        protected void gvMarcas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMarcas.EditIndex = -1;
            CargarMarcas();
        }

        protected void gvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);
            string nuevoNombre = ((TextBox)gvMarcas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            // Validar si el nombre ya existe
            if (negocioMarca.ExisteNombreMarca(nuevoNombre, id))
            {
                lblError.Text = "El nombre de la marca ya existe. Por favor, elige otro nombre.";
                lblError.Visible = true;
                return;
            }

            Marca marcaActualizada = new Marca
            {
                ID = id,
                Nombre = nuevoNombre
            };

            negocioMarca.modificarMarca(marcaActualizada);
            gvMarcas.EditIndex = -1;
            CargarMarcas();
        }


        protected void gvMarcas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);
            negocioMarca.eliminarMarca(id);
            CargarMarcas();
        }
    }
}