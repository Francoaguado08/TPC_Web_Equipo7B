using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Web
{
    public partial class _Default : Page
    {
        public List<Articulo> listaArticulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                obtenerProductos();
                inicializarFiltros();
            }
        }

        private void obtenerProductos()
        {
            try
            {
                ArticuloNegocio articulos = new ArticuloNegocio();
                ImagenesNegocio imagenes = new ImagenesNegocio();

                List<Imagen> misImagenes = imagenes.listar();
                listaArticulo = articulos.listar();

                imagenes.vincularImagenes(listaArticulo, misImagenes);

                Session["articulos"] = listaArticulo;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar productos: {ex.Message}');</script>");
            }
        }

        private void inicializarFiltros()
        {
            ddlFiltrarPor.Items.Clear();
            ddlFiltrarPor.Items.Add("Precio");
            ddlFiltrarPor.Items.Add("Categoría");
            ddlFiltrarPor.Items.Add("Marca");
            ddlFiltrarPor.Items.Insert(0, new ListItem("Selecciona...", ""));

            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Insert(0, new ListItem("Selecciona...", ""));
        }

        protected void ddlFiltrarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Add(new ListItem("Selecciona...", ""));

            if (ddlFiltrarPor.SelectedValue == "Precio")
            {
                ddlCriterio.Items.Add("Ascendente");
                ddlCriterio.Items.Add("Descendente");
            }
            else if (ddlFiltrarPor.SelectedValue == "Marca")
            {
                MarcasNegocio marcasNegocio = new MarcasNegocio();
                foreach (var marca in marcasNegocio.listar())
                {
                    ddlCriterio.Items.Add(marca.Nombre);
                }
            }
            else if (ddlFiltrarPor.SelectedValue == "Categoría")
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                foreach (var categoria in categoriaNegocio.listar())
                {
                    ddlCriterio.Items.Add(categoria.Nombre);
                }
            }
        }

        protected void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio articulos = new ArticuloNegocio();
                ImagenesNegocio imagenes = new ImagenesNegocio();
                List<Imagen> misImagenes = imagenes.listar();

                string campo = ddlFiltrarPor.SelectedValue;
                string criterio = ddlCriterio.SelectedValue;

                if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(criterio))
                {
                    listaArticulo = articulos.listarFiltrados(campo, criterio);
                }
                else
                {
                    Response.Write("<script>alert('Selecciona un filtro válido.');</script>");
                    return;
                }

                imagenes.vincularImagenes(listaArticulo, misImagenes);
                Session["articulos"] = listaArticulo;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al aplicar filtro: {ex.Message}');</script>");
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlFiltrarPor.SelectedIndex = 0;
            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Add(new ListItem("Selecciona...", ""));
            obtenerProductos();
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = tbxBuscar.Text;

                if (!string.IsNullOrEmpty(textoBusqueda) && textoBusqueda.Length >= 2)
                {
                    listaArticulo = ((List<Articulo>)Session["articulos"]).FindAll(x =>
                        x.Nombre.ToUpper().Contains(textoBusqueda.ToUpper()));
                }
                else
                {
                    obtenerProductos();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al buscar productos: {ex.Message}');</script>");
            }
        }
    }
}