using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo artiSeleccionado; //Variable para guardar el arti seleccionado que me pasaron desde el home.
        public void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                // Obtiene el ID del artículo de la cadena de consulta
                int id = int.Parse(Request.QueryString["id"]);

                // Busca el artículo en la lista almacenada en la sesión
                artiSeleccionado = ((List<Articulo>)Session["articulos"]).Find(x => x.ID == id);

                if (artiSeleccionado != null)
                {
                    // Muestra los detalles del artículo en los controles de texto
                    txtNombre.Text = artiSeleccionado.Nombre; // Sin formato HTML
                    txtCodigo.Text = $"Código: {artiSeleccionado.Codigo}"; // Sin formato HTML
                    txtPrecio.Text = $"ARS {Math.Round(artiSeleccionado.Precio, 2):N2}"; // Formato numérico
                    txtDescripcion.Text = artiSeleccionado.Descripcion;
                    txtMarca.Text = $"Marca: {artiSeleccionado.Marca}";
                    txtCategoria.Text = $"Categoría: {artiSeleccionado.Categoria}";
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