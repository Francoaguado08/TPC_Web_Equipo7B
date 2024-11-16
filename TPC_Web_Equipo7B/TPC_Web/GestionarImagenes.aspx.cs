using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class GestionarImagenes : System.Web.UI.Page
    {

        private Articulo articuloActual;
        private List<string> imagenes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del artículo desde el QueryString
                int idArticulo = int.Parse(Request.QueryString["id"]);

                // Recuperar el artículo desde la sesión o base de datos
                List<Articulo> listaArticulos = (List<Articulo>)Session["listaArticulos"];
                articuloActual = listaArticulos?.Find(a => a.ID == idArticulo);

                if (articuloActual == null)
                {
                    Response.Redirect("AdministrarArticulos.aspx", false);
                    return;
                }

                // Mostrar datos del artículo
                lblArticulo.Text += articuloActual.Nombre;

                // Cargar las URLs de las imágenes
                imagenes = articuloActual.ImagenURL;
                CargarImagenes();
            }
        }

        private void CargarImagenes()
        {
            gvImagenes.DataSource = imagenes.Select(url => new { URL = url }).ToList();
            gvImagenes.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNuevaImagen.Text))
            {
                // Agregar la nueva URL a la lista de imágenes
                imagenes.Add(txtNuevaImagen.Text);

                // Actualizar el artículo en la sesión
                ActualizarArticuloEnSesion();

                // Recargar las imágenes
                CargarImagenes();
                txtNuevaImagen.Text = string.Empty;
            }
        }

        protected void gvImagenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtener el índice de la fila
                int index = Convert.ToInt32(e.CommandArgument);

                // Eliminar la URL de la lista
                imagenes.RemoveAt(index);

                // Actualizar el artículo en la sesión
                ActualizarArticuloEnSesion();

                // Recargar las imágenes
                CargarImagenes();
            }
        }

        private void ActualizarArticuloEnSesion()
        {
            // Guardar los cambios en la sesión
            List<Articulo> listaArticulos = (List<Articulo>)Session["listaArticulos"];
            Articulo articuloEnSesion = listaArticulos?.Find(a => a.ID == articuloActual.ID);

            if (articuloEnSesion != null)
            {
                articuloEnSesion.ImagenURL = imagenes;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarArticulos.aspx", false);
        }
















    }
}