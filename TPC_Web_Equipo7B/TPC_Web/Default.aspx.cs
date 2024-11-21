using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.ComponentModel;

namespace TPC_Web
{
    public partial class _Default : Page
    {
        public List<Articulo> listaArticulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack || Request.QueryString["reload"] == "true")
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

                // Recargar datos desde la base de datos
                List<Imagen> misImagenes = imagenes.listar();
                listaArticulo = articulos.listar();

                // Vincular imágenes con artículos
                imagenes.vincularImagenes(listaArticulo, misImagenes);

                // Actualizar la sesión con los datos más recientes
                Session["articulos"] = listaArticulo;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write($"<script>alert('Error al cargar productos: {ex.Message}');</script>");
            }
        }

        private void inicializarFiltros()
        {
            // Opciones de filtro disponibles
            ddlFiltrarPor.Items.Add("Precio");
            ddlFiltrarPor.Items.Add("Categoría");
            ddlFiltrarPor.Items.Add("Marca");

            // Valor predeterminado vacío
            ddlFiltrarPor.Items.Insert(0, new ListItem("Selecciona...", ""));
            ddlFiltrarPor.SelectedIndex = 0;

            // Inicializar ddlCriterio con un valor vacío
            ddlCriterio.Items.Insert(0, new ListItem("Selecciona...", ""));
            ddlCriterio.SelectedIndex = 0;
        }

        protected void ddlFiltrarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Guardar selección del campo en sesión
            Session["campo"] = ddlFiltrarPor.SelectedValue;

            // Limpiar opciones actuales de criterio y cargar nuevas según el campo seleccionado
            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Add(new ListItem("Selecciona...", ""));

            if (ddlFiltrarPor.SelectedValue == "Precio")
            {
                ddlCriterio.Items.Add("Ascendente");
                ddlCriterio.Items.Add("Descendente");
            }
            else if (ddlFiltrarPor.SelectedValue == "Marca")
            {
                // Cargar marcas desde la base de datos
                MarcasNegocio marcasNegocio = new MarcasNegocio();
                List<Marca> marcas = marcasNegocio.listar();
                foreach (var marca in marcas)
                {
                    ddlCriterio.Items.Add(marca.Nombre);
                }
            }
            else if (ddlFiltrarPor.SelectedValue == "Categoría")
            {
                // Cargar categorías desde la base de datos
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.listar();
                foreach (var categoria in categorias)
                {
                    ddlCriterio.Items.Add(categoria.Nombre);
                }
            }

            ddlCriterio.SelectedIndex = 0; // Restablecer selección de criterio
        }

        protected void ddlCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Guardar selección del criterio en sesión
            Session["criterio"] = ddlCriterio.SelectedValue;
        }

        protected void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio articulos = new ArticuloNegocio();
                ImagenesNegocio imagenes = new ImagenesNegocio();
                List<Imagen> misImagenes = imagenes.listar();

                // Obtener valores de los filtros
                string campo = ddlFiltrarPor.SelectedValue;
                string criterio = ddlCriterio.SelectedValue;

                if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(criterio))
                {
                    // Aplicar filtros
                    listaArticulo = articulos.listarFiltrados(campo, criterio);
                }
                else
                {
                    // Si no hay filtro, recargar todos los productos
                    listaArticulo = articulos.listar();
                }

                // Vincular imágenes
                imagenes.vincularImagenes(listaArticulo, misImagenes);

                // Actualizar la sesión con los datos filtrados
                Session["articulos"] = listaArticulo;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al aplicar filtro: {ex.Message}');</script>");
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            // Restablecer selección de filtros
            ddlFiltrarPor.SelectedIndex = 0;
            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Add(new ListItem("Selecciona...", ""));
            ddlCriterio.SelectedIndex = 0;

            // Recargar todos los productos
            obtenerProductos();
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = tbxBuscar.Text;

                if (textoBusqueda.Length >= 2)
                {
                    // Filtrar artículos por nombre
                    listaArticulo = ((List<Articulo>)Session["articulos"]).FindAll(x =>
                        x.Nombre.ToUpper().Contains(textoBusqueda.ToUpper()));
                }
                else
                {
                    // Mostrar todos los artículos si el texto es muy corto
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