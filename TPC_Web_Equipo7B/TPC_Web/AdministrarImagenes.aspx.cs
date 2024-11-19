using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarImagenes : System.Web.UI.Page
    {
        private ImagenesNegocio imagenNegocio = new ImagenesNegocio();
        private int IDArti;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar que el ID del artículo esté presente en la URL
                if (Request.QueryString["ID"] != null)
                {
                    IDArti = Convert.ToInt32(Request.QueryString["ID"]);
                }
                else
                {
                    lblError.Text = "Error: No se encontró el ID del artículo.";
                    lblError.Visible = true;
                    return;
                }

                CargarDatosArticulo();
                CargarImagenes();
            }
        }

        private void CargarDatosArticulo()
        {
            // Obtengo el artículo completo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = articuloNegocio.ObtenerPorId(IDArti);

            if (articulo != null)
            {
                lblArticulo.Text = $"Administrando imágenes del artículo: {articulo.Nombre}";
            }
            else
            {
                // Redirigir en caso de que el artículo no exista
                Response.Redirect("AdministrarArticulos.aspx");
            }
        }

        private void CargarImagenes()
        {
            List < Imagen > imagenes = imagenNegocio.listarImagenesArticuloSeleccionado(IDArti);

            // Verificar si se obtienen imágenes después de la eliminación
            if (imagenes.Count == 0)
            {
                lblError.Visible = true;
                lblError.Text = "No hay imágenes disponibles para este artículo.";
            }
            else
            {
                gvImagenes.DataSource = imagenes;
                gvImagenes.DataBind();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                // Obtener el botón que disparó el evento y su CommandArgument
                Button btnEliminar = (Button)sender;
                int imagenId = Convert.ToInt32(btnEliminar.CommandArgument);  // Obtener el ID de la imagen

                // Llamar al método de negocio para eliminar la imagen
                imagenNegocio.eliminar(imagenId);

                // Recargar la lista de imágenes después de la eliminación
                CargarImagenes();

                // Ocultar los mensajes de error si todo salió bien
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si ocurre un problema
                lblError.Visible = true;
                lblError.Text = "Ocurrió un error al eliminar la imagen: " + ex.Message;
            }



        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear una nueva imagen con la URL proporcionada
                Imagen nuevaImagen = new Imagen
                {
                    IDArticulo = IDArti,  // El ID del artículo actual
                    ImagenURl = txtNuevaImagenURL.Text
                };

                // Llamar al método de negocio para agregar la nueva imagen
                imagenNegocio.agregarImagen(nuevaImagen);

                // Recargar la lista de imágenes
                CargarImagenes();

                // Limpiar el campo de texto
                txtNuevaImagenURL.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si ocurre un problema
                lblError.Visible = true;
                lblError.Text = "Ocurrió un error al agregar la imagen: " + ex.Message;
            }
        }

    }
}