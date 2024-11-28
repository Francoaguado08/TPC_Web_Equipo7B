using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
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
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                ddlCategorias.SelectedValue == null ||
                ddlMarcas.SelectedValue == null)
            {
                lblError.Text = "Por favor, complete todos los campos antes de continuar.";
                lblError.Visible = true;
                return;
            }

            // Validar el precio
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                lblError.Text = "El precio debe ser un número válido y mayor a 0.";
                lblError.Visible = true;
                return;
            }

            // Validar el stock
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                lblError.Text = "El stock debe ser un número entero no negativo.";
                lblError.Visible = true;
                return;
            }

            // Asignar valores del formulario
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            int categoriaId = Convert.ToInt32(ddlCategorias.SelectedValue);
            int marcaId = Convert.ToInt32(ddlMarcas.SelectedValue);

            // Crear objeto Articulo
            Articulo articulo = new Articulo
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]),
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Stock = stock,
                Categoria = new Categoria { ID = categoriaId },
                Marca = new Marca { ID = marcaId }
            };

            // Validar que el código no esté duplicado
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaExistente = articuloNegocio.listar();
            if (listaExistente.Exists(a => a.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && a.ID != articulo.ID))
            {
                lblError.Text = "El código de producto ya existe. Por favor, ingrese un código diferente.";
                lblError.Visible = true;
                return;
            }

            // Actualizar el artículo
            articuloNegocio.modificar(articulo);
            Response.Redirect("AdministrarArticulos.aspx");
        }

        private void CargarArticulo(int ID)
        {
            // Obtener el artículo completo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = articuloNegocio.ObtenerPorId(ID);

            if (articulo != null)
            {
                // Completar los controles del formulario con los datos
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                txtStock.Text = articulo.Stock.ToString(); // Mostrar el stock actual

                // Seleccionar la categoría y marca
                ddlCategorias.SelectedValue = articulo.Categoria.ID.ToString();
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
            List<Marca> marcas = marcaNegocio.listar();

            if (marcas != null && marcas.Count > 0)
            {
                ddlMarcas.DataSource = marcas;
                ddlMarcas.DataTextField = "Nombre";
                ddlMarcas.DataValueField = "ID";
                ddlMarcas.DataBind();
            }
            else
            {
                ddlMarcas.Items.Add(new ListItem("No hay marcas disponibles", "0"));
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.listar();

            if (categorias != null && categorias.Count > 0)
            {
                ddlCategorias.DataSource = categorias;
                ddlCategorias.DataTextField = "Nombre";
                ddlCategorias.DataValueField = "ID";
                ddlCategorias.DataBind();
            }
            else
            {
                ddlCategorias.Items.Add(new ListItem("No hay categorías disponibles", "0"));
            }
        }
    }
}
