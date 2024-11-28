using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
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
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                cargarCategorias();
                cargarMarcas();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    List<Articulo> temporal = (List<Articulo>)Session["listaArticulos"];
                    Articulo seleccionado = temporal.Find(x => x.ID == id);

                    txtCodigo.Text = seleccionado.Codigo;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtPrecio.Text = seleccionado.Precio.ToString();
                    ddlCategorias.SelectedValue = seleccionado.Categoria.ID.ToString();
                    ddlMarcas.SelectedValue = seleccionado.Marca.ID.ToString();
                    txtStock.Text = seleccionado.Stock.ToString();
                }
            }
        }

        private void cargarCategorias()
        {
            List<Categoria> categorias = negocioCategoria.listar();
            ddlCategorias.DataSource = categorias;
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataValueField = "ID";
            ddlCategorias.DataBind();
        }

        private void cargarMarcas()
        {
            List<Marca> marcas = negocioMarca.listar();
            ddlMarcas.DataSource = marcas;
            ddlMarcas.DataTextField = "Nombre";
            ddlMarcas.DataValueField = "ID";
            ddlMarcas.DataBind();
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
                lblError.Text = "El stock debe ser un número entero positivo.";
                lblError.Visible = true;
                return;
            }

            Articulo articulo = new Articulo
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = precio,
                Stock = stock,
                Categoria = new Categoria
                {
                    ID = int.Parse(ddlCategorias.SelectedValue),
                    Nombre = ddlCategorias.SelectedItem.Text
                },
                Marca = new Marca
                {
                    ID = int.Parse(ddlMarcas.SelectedValue),
                    Nombre = ddlMarcas.SelectedItem.Text
                }
            };

            // Validar si el código ya existe
            List<Articulo> listaExistente = (List<Articulo>)Session["listaArticulos"] ?? negocioArticulo.listar();
            if (listaExistente.Exists(a => a.Codigo.Equals(articulo.Codigo, StringComparison.OrdinalIgnoreCase)))
            {
                lblError.Text = "El código de producto ya existe. Por favor, ingrese un código diferente.";
                lblError.Visible = true;
                return;
            }

            // Agregar el artículo a la lista y la base de datos
            listaExistente.Add(articulo);
            Session["listaArticulos"] = listaExistente;

            negocioArticulo.agregar(articulo);
            Response.Redirect("AdministrarArticulos.aspx", false);
        }
    }
}
