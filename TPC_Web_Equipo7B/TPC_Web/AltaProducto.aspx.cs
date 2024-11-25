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
            Articulo articulo = new Articulo
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text
            };

            if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                articulo.Precio = precio;
            }

            if (int.TryParse(ddlCategorias.SelectedValue, out int categoriaId))
            {
                articulo.Categoria = new Categoria
                {
                    ID = categoriaId,
                    Nombre = ddlCategorias.SelectedItem.Text
                };
            }

            if (int.TryParse(ddlMarcas.SelectedValue, out int marcaId))
            {
                articulo.Marca = new Marca
                {
                    ID = marcaId,
                    Nombre = ddlMarcas.SelectedItem.Text
                };
            }

            List<Articulo> temporal = (List<Articulo>)Session["listaArticulos"] ?? new List<Articulo>();
            temporal.Add(articulo);
            Session["listaArticulos"] = temporal;

            negocioArticulo.agregar(articulo);
            Response.Redirect("AdministrarArticulos.aspx", false);
        }
    }
}