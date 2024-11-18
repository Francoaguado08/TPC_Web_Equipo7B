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
                // Obtener el ID del artículo de la URL
                IDArti= Convert.ToInt32(Request.QueryString["ID"]);
                CargarDatosArticulo();
                CargarImagenes();
            }
        }

        private void CargarDatosArticulo()
        {
            

            // Obtengo el artículo completo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = articuloNegocio.ObtenerPorId(   IDArti);

            if (articulo != null)
            {
                lblArticulo.Text = $"Administrando imágenes del artículo: {articulo.Nombre}";
            }
        }

        private void CargarImagenes()
        {
            // Cargar imágenes relacionadas con el artículo seleccionado
            List<Imagen> imagenes = imagenNegocio.listarImagenesArticuloSeleccionado(IDArti);
            gvImagenes.DataSource = imagenes;
            gvImagenes.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Mostrar el formulario para agregar nueva imagen
            pnlFormulario.Visible = true;
            lblFormulario.Text = "Agregar Nueva Imagen";
            txtImagenURL.Text = string.Empty;
            ViewState["EditarImagenId"] = null; // Limpiar cualquier edición previa
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener la URL de la imagen ingresada
            string urlImagen = txtImagenURL.Text.Trim();

            if (string.IsNullOrEmpty(urlImagen))
            {
                // Validar que la URL no esté vacía
                lblFormulario.Text = "La URL de la imagen no puede estar vacía.";
                return;
            }

            if (ViewState["EditarImagenId"] == null)
            {
                // Agregar nueva imagen
                Imagen nuevaImagen = new Imagen
                {
                    IDArticulo = IDArti,
                    ImagenURl = urlImagen
                };

                imagenNegocio.agregarImagen(nuevaImagen);
            }
            else
            {
                // Actualizar imagen existente
                int imagenId = Convert.ToInt32(ViewState["EditarImagenId"]);
                Imagen imagenActualizada = new Imagen
                {
                    ID = imagenId,
                    IDArticulo = IDArti,
                    ImagenURl = urlImagen
                };

                imagenNegocio.actualizarImagen(imagenActualizada);
            }

            // Ocultar el formulario y recargar la lista
            pnlFormulario.Visible = false;
            CargarImagenes();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            // Obtener el botón que disparó el evento y su CommandArgument
            Button btnEditar = (Button)sender;
            int imagenId = Convert.ToInt32(btnEditar.CommandArgument);

            // Obtener la imagen a editar
            Imagen imagen = imagenNegocio.listar().Find(img => img.ID == imagenId);

            if (imagen != null)
            {
                // Cargar los datos en el formulario para editar
                txtImagenURL.Text = imagen.ImagenURl;
                pnlFormulario.Visible = true;
                lblFormulario.Text = "Editar Imagen";
                ViewState["EditarImagenId"] = imagen.ID; // Guardar el ID de la imagen en ViewState
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el botón que disparó el evento y su CommandArgument
            Button btnEliminar = (Button)sender;
            int imagenId = Convert.ToInt32(btnEliminar.CommandArgument);

            // Eliminar la imagen
            imagenNegocio.eliminar(imagenId);

            // Recargar la lista
            CargarImagenes();
        }
    }
}