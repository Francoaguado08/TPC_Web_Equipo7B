using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace TPC_Web
{
    public partial class EliminarProducto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del producto desde el query string
                string idQuery = Request.QueryString["id"];
                if (int.TryParse(idQuery, out int idProducto))
                {
                    CargarProducto(idProducto);
                }
                else
                {
                    // Redirigir si no hay un ID válido
                    Response.Redirect("AdministrarArticulos.aspx");
                }
            }
        }

        private void CargarProducto(int idProducto)
        {
            try
            {
                // Instanciar la capa de negocio
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerPorId(idProducto);

                if (articulo != null)
                {
                    // Mostrar el nombre del producto en el label
                    lblNombreProducto.Text = articulo.Nombre;
                    // Guardar el ID en un campo oculto
                    hfProductoID.Value = articulo.ID.ToString();
                }
                else
                {
                    // Si no se encuentra el producto, redirigir
                    Response.Redirect("AdministrarArticulos.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores (opcional: loguear)
                lblNombreProducto.Text = "Error al cargar el producto.";
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recuperar el ID del producto del campo oculto
                if (int.TryParse(hfProductoID.Value, out int idProducto))
                {
                    // Eliminar el producto usando la capa de negocio
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminarArticulo(idProducto);

                    // Redirigir después de eliminar
                    Response.Redirect("AdministrarArticulos.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores (opcional: mostrar un mensaje)
                lblNombreProducto.Text = "Error al eliminar el producto.";
            }
        }
    }
}
