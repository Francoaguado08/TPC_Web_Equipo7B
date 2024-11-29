using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TPC_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo artiSeleccionado; //Variable para guardar el artículo seleccionado desde el home.

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si existe el parámetro "id" en la consulta
                string idParam = Request.QueryString["id"];

                if (string.IsNullOrEmpty(idParam) || !int.TryParse(idParam, out int id))
                {
                    // Si no hay un id válido, redirige a "Default.aspx"
                    Response.Redirect("Default.aspx", false);
                    return; // Asegura que el código no continúe ejecutándose
                }

                // Busca el artículo en la lista almacenada en la sesión
                artiSeleccionado = ((List<Articulo>)Session["articulos"])?.Find(x => x.ID == id);

                if (artiSeleccionado != null)
                {
                    // Muestra los detalles del artículo en los controles de texto
                    txtNombre.Text = artiSeleccionado.Nombre; // Sin formato HTML
                    txtCodigo.Text = $"Código: {artiSeleccionado.Codigo}"; // Sin formato HTML
                    txtPrecio.Text = $"ARS {Math.Round(artiSeleccionado.Precio, 2):N2}"; // Formato numérico
                    txtDescripcion.Text = artiSeleccionado.Descripcion;
                    txtMarca.Text = $"Marca: {artiSeleccionado.Marca}";
                    txtCategoria.Text = $"Categoría: {artiSeleccionado.Categoria}";
                    lblStockDisponible.Text = artiSeleccionado.Stock.ToString();
                }
                else
                {
                    // Redirige si no se encuentra el artículo
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
    }
}
