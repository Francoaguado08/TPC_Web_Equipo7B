using System;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class AltaCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreCategoria = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreCategoria))
            {
                lblMensaje.Text = "El nombre de la categoría no puede estar vacío.";
                return;
            }

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                Categoria nuevaCategoria = new Categoria { Nombre = nombreCategoria };
                negocio.agregarCategoria(nuevaCategoria);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Categoría guardada exitosamente.";
                txtNombre.Text = ""; // Limpiar el campo
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarCategoria.aspx");
        }
    }
}