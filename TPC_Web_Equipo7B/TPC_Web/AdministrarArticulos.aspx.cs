using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_Web
{
    public partial class AdministrarArticulos : System.Web.UI.Page
    {
        ArticuloNegocio negocioArticulo = new ArticuloNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está logueado y tiene permisos de administrador
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                // Redirigir al inicio si no está logueado o no tiene permiso de administrador
                Response.Redirect("Default.aspx");
            }

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
            Response.Redirect("Administrar.aspx");
        }
    }
}