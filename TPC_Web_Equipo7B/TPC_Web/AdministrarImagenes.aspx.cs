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
        ImagenesNegocio imagenNegocio = new ImagenesNegocio();
         int IDArticulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    IDArticulo = Convert.ToInt32(Request.QueryString["ID"]);
                    Session["IDArticulo"] = IDArticulo; // Guardar el ID en la sesión
                }
                else
                {
                    Response.Redirect("AdministrarArticulos.aspx");
                }

                CargarDatosArticulo();
                CargarImagenes();
            }
            else
            {
                // Recuperar el ID desde la sesión para mantenerlo disponible
                if (Session["IDArticulo"] != null)
                {
                    IDArticulo = Convert.ToInt32(Session["IDArticulo"]);
                }
            }
        }

        private void CargarDatosArticulo()
        {
            // Obtengo el artículo completo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = articuloNegocio.ObtenerPorId(IDArticulo);

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
            List < Imagen > imagenes = imagenNegocio.listarImagenesArticuloSeleccionado(IDArticulo);

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
                if (IDArticulo <= 0)
                {
                    throw new Exception("El ID del artículo no es válido. Verifica que la página tiene el parámetro correcto en la URL.");
                }

                // Crear una nueva imagen con la URL proporcionada
                Imagen nuevaImagen = new Imagen
                {
                    IDArticulo = IDArticulo,  // El ID del artículo actual
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
                lblError.Visible = true;
                lblError.Text = "Ocurrió un error al agregar la imagen: " + ex.Message;
            }


        }



        




    }
}