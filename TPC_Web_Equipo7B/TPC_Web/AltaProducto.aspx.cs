using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace TPC_Web
{
    public partial class AltaProducto : System.Web.UI.Page
    {
        ArticuloNegocio negocioArticulo = new ArticuloNegocio();
        CategoriaNegocio negocioCategoria = new CategoriaNegocio();
        MarcasNegocio negocioMarca = new MarcasNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar las categorías y marcas en los dropdowns
                cargarCategorias();
                cargarMarcas();

                // Si se está editando un artículo
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    List<Articulo> temporal = (List<Articulo>)Session["listaArticulos"];
                    Articulo seleccionado = temporal.Find(x => x.ID == id);

                    // Mostrar datos del artículo en los controles
                    txtCodigo.Text = seleccionado.Codigo;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtPrecio.Text = seleccionado.Precio.ToString();

                    ddlCategorias.SelectedValue = seleccionado.Categoria.ID.ToString();
                    ddlMarcas.SelectedValue = seleccionado.Marca.ID.ToString();

                    // Mostrar las URLs existentes (concatenarlas en el TextBox o mostrarlas de otra forma)
                    txtUrlImagen.Text = string.Join(", ", seleccionado.ImagenURL);
                }
            }
        }

        private void cargarCategorias()
        {
            // Cargar las categorías desde la base de datos
            List<Categoria> categorias = negocioCategoria.listar();
            ddlCategorias.DataSource = categorias;
            ddlCategorias.DataTextField = "Descripcion";
            ddlCategorias.DataValueField = "ID";
            ddlCategorias.DataBind();
        }

        private void cargarMarcas()
        {
            // Cargar las marcas desde la base de datos
            List<Marca> marcas = negocioMarca.listar();
            ddlMarcas.DataSource = marcas;
            ddlMarcas.DataTextField = "Descripcion";
            ddlMarcas.DataValueField = "ID";
            ddlMarcas.DataBind();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Crear una nueva instancia de Articulo
            Articulo articulo = new Articulo();

            // Asignar los valores del formulario a las propiedades del artículo
            articulo.Codigo = txtCodigo.Text;
            articulo.Nombre = txtNombre.Text;
            articulo.Descripcion = txtDescripcion.Text;

            if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                articulo.Precio = precio;
            }


            // Asignar la categoría seleccionada
            if (int.TryParse(ddlCategorias.SelectedValue, out int categoriaId))
            {
                articulo.Categoria = new Categoria
                {
                    ID = categoriaId,
                    Descripcion = ddlCategorias.SelectedItem.Text
                };
            }

            // Asignar la marca seleccionada
            if (int.TryParse(ddlMarcas.SelectedValue, out int marcaId))
            {
                articulo.Marca = new Marca
                {
                    ID = marcaId,
                    Descripcion = ddlMarcas.SelectedItem.Text
                };
            }

            // Asignar la URL de la imagen
            if (!string.IsNullOrEmpty(txtUrlImagen.Text))
            {
                articulo.ImagenURL.Add(txtUrlImagen.Text);
            }

            // Verificar si la sesión contiene la lista de artículos
            List<Articulo> temporal = (List<Articulo>)Session["listaArticulos"];

            // Si es la primera vez o no existe la lista, inicialízala
            if (temporal == null)
            {
                temporal = new List<Articulo>();
                Session["listaArticulos"] = temporal; // Asignamos la lista vacía a la sesión
            }

            // Agregar el artículo a la lista en la sesión
            temporal.Add(articulo);

            negocioArticulo.agregar(articulo);

            // Redirigir a la página de administración de artículos
            Response.Redirect("AdministrarArticulos.aspx", false);


        }
    }
}