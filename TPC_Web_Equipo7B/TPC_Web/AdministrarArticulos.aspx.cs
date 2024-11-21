using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TPC_Web
{
    public partial class AdministrarArticulos : System.Web.UI.Page
    {


        ArticuloNegocio negocioArticulo = new ArticuloNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los artículos en el GridView
                CargarArticulos();
            }
        }

        private void CargarArticulos()
        {
            // Obtener los artículos de la base de datos o la lista en sesión
            List<Articulo> listaArticulos = negocioArticulo.listar();

            // Asignar la lista al GridView
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }

       
        
        
        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            // Redirige a la página de alta de artículo
            Response.Redirect("AltaProducto.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?reload=true");
        }
    }
}