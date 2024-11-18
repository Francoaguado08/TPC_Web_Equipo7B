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
    public partial class EditarProducto : System.Web.UI.Page
    {
       
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                 
                CargarCategorias();
                CargarMarcas();



                // Obtener el ID del artículo de la URL
                int idArticulo = Convert.ToInt32(Request.QueryString["ID"]);

                CargarArticulo(idArticulo);
                // Lógica para cargar los datos del artículo según el ID
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
           
            // Obtener los datos del formulario (1)
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio = Convert.ToDecimal(txtPrecio.Text);
            int categoriaId = Convert.ToInt32(ddlCategorias.SelectedValue);
            int marcaId = Convert.ToInt32(ddlMarcas.SelectedValue);

            // Crear un objeto Articulo con los datos modificados
            Articulo articulo = new Articulo
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]), // Obtener el ID del artículo desde la URL
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Categoria = new Categoria { ID = categoriaId },
                Marca = new Marca { ID = marcaId }
            };

            // Llamar al método para actualizar el artículo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            // Actualizar los datos generales del artículo
            articuloNegocio.modificar(articulo);

            // Actualizar la categoría del artículo
            articuloNegocio.modificarCategoriaArticulo(articulo);

            // Actualizar la marca del artículo
            articuloNegocio.modificarMarcaArticulo(articulo);

            // Redirigir o mostrar mensaje de éxito
            Response.Redirect("Exito.aspx"); // O la página que prefieras






        }


        private void CargarArticulo(int ID)
        {
            // Obtengo el artículo completo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = articuloNegocio.ObtenerPorId(ID);

            if (articulo != null)
            {
                // Completa los controles del formulario con los datos
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();


                // MARCAS Y CATEGORIAS:
                // Selecciona la categoría del artículo
                ddlCategorias.SelectedValue = articulo.Categoria.ID.ToString();

                // Selecciona la marca del artículo
                ddlMarcas.SelectedValue = articulo.Marca.ID.ToString();
            }
            else
            {
                Response.Redirect("AdministrarArticulos.aspx", false); 
            }

        }







        private void CargarMarcas()
        {
            MarcasNegocio marcaNegocio = new MarcasNegocio();
            List<Marca> marcas = marcaNegocio.listar(); // Devuelve una lista de todas las marcas

            if (marcas != null && marcas.Count > 0)
            {
                ddlMarcas.DataSource = marcas;
                ddlMarcas.DataTextField = "Nombre"; // Campo que se muestra //pero atributo que tenemos tanto en el CODIGO Y EN LA BD.
                ddlMarcas.DataValueField = "ID";   // Valor asociado al item
                ddlMarcas.DataBind();
            }
            else
            {
                // Si no hay marcas disponibles, puedes añadir un mensaje o manejarlo de alguna forma.
                ddlMarcas.Items.Add(new ListItem("No hay marcas disponibles", "0"));
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.listar(); // Devuelve una lista de todas las categorías

            if (categorias != null && categorias.Count > 0)
            {
                ddlCategorias.DataSource = categorias;
                ddlCategorias.DataTextField = "Nombre"; // Campo que se muestra
                ddlCategorias.DataValueField = "ID";   // Valor asociado al item
                ddlCategorias.DataBind();
            }
            else
            {
                // Si no hay categorías disponibles, puedes añadir un mensaje o manejarlo de alguna forma.
                ddlCategorias.Items.Add(new ListItem("No hay categorías disponibles", "0"));
            }
        }




    }
}