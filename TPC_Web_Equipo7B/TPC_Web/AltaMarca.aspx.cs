using System;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class AltaMarca : System.Web.UI.Page
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
            string nombreMarca = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreMarca))
            {
                lblMensaje.Text = "El nombre de la marca no puede estar vacío.";
                return;
            }

            try
            {
                MarcasNegocio negocio = new MarcasNegocio();

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
            Response.Redirect("AdministrarMarca.aspx");
        }
    }
}