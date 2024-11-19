using System;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class AltaMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicialización de la página si es necesario
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreMarca = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreMarca))
            {
                lblMensaje.Text = "El nombre de la marca no puede estar vacío.";
                return;
            }

            try
            {
                // Crear instancia de la clase MarcasNegocio
                MarcasNegocio negocio = new MarcasNegocio();

                // Crear nueva marca y agregarla
                Marca nuevaMarca = new Marca { Nombre = nombreMarca };
                negocio.agregarMarcas(nuevaMarca);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Marca guardada exitosamente.";
                txtNombre.Text = ""; // Limpiar el campo
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Cambia "AdministrarMarcas.aspx" por la ruta de tu página de administración
            Response.Redirect("AdministrarMarca.aspx");
        }
    }
}
