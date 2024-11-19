using System;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class AltaCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicialización de la página si es necesario
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
                // Crear instancia de la clase CategoriaNegocio
                CategoriaNegocio negocio = new CategoriaNegocio();

                // Crear nueva categoría y agregarla
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
            // Cambia "AdministrarCategorias.aspx" por la ruta de tu página de administración
            Response.Redirect("AdministrarCategoria.aspx");
        }
    }
}
