using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializar o recuperar el carrito de compras desde la sesión
                CarritoCompras miCarrito = (CarritoCompras)Session["compras"] ?? new CarritoCompras();
                Session["compras"] = miCarrito;

                // Verificar si hay un parámetro "id" en la URL para agregar un artículo al carrito
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    List<Articulo> articulos = (List<Articulo>)Session["articulos"]; // Recuperar lista de artículos

                    if (articulos != null)
                    {
                        Articulo artAgregado = articulos.Find(x => x.ID == id);

                        if (artAgregado != null)
                        {
                            // Validar si hay stock disponible antes de agregarlo
                            if (artAgregado.Stock > 0)
                            {
                                miCarrito.AgregarProducto(artAgregado);
                            }
                            else
                            {
                                lblMensajeError.Text = "El producto no tiene stock disponible.";
                                lblMensajeError.Visible = true;
                            }
                        }
                    }
                }

                // Enlazar el carrito al GridView
                BindGridView();
                ActualizarTotalGeneral(miCarrito);
            }
        }

        protected void dgvCompras_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCompras.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void dgvCompras_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];
            GridViewRow row = dgvCompras.Rows[e.RowIndex];
            int id = Convert.ToInt32(dgvCompras.DataKeys[e.RowIndex].Value);

            // Obtener la cantidad nueva del TextBox
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            int nuevaCantidad;

            if (int.TryParse(txtCantidad.Text, out nuevaCantidad) && nuevaCantidad > 0)
            {
                Articulo producto = miCarrito.ObtenerProductos().Find(p => p.ID == id);

                if (producto != null)
                {
                    // Validar contra el stock disponible
                    if (nuevaCantidad <= producto.Stock)
                    {
                        producto.Cantidad = nuevaCantidad;
                        lblMensajeError.Visible = false;
                    }
                    else
                    {
                        lblMensajeError.Text = $"No puedes agregar más de {producto.Stock} unidades del producto '{producto.Nombre}'.";
                        lblMensajeError.Visible = true;
                    }
                }

                Session["compras"] = miCarrito;
                dgvCompras.EditIndex = -1;
                BindGridView();
                ActualizarTotalGeneral(miCarrito);
            }
        }

        protected void dgvCompras_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvCompras.EditIndex = -1;
            BindGridView();
        }

        private void BindGridView()
        {
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];
            dgvCompras.DataSource = miCarrito.ObtenerProductos();
            dgvCompras.DataBind();
        }

        private void ActualizarTotalGeneral(CarritoCompras miCarrito)
        {
            decimal totalGeneral = miCarrito.ObtenerProductos().Sum(a => a.Precio * a.Cantidad);
            lblTotalGeneral.Text = "Total: " + totalGeneral.ToString("C");
        }

        protected void btnCkeckout_Click(object sender, EventArgs e)
        {
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

            if (miCarrito != null && miCarrito.ObtenerProductos().Count > 0)
            {
                // Validar stock antes de proceder
                foreach (var producto in miCarrito.ObtenerProductos())
                {
                    if (producto.Cantidad > producto.Stock)
                    {
                        lblMensajeError.Text = $"El producto '{producto.Nombre}' no tiene suficiente stock.";
                        lblMensajeError.Visible = true;
                        return;
                    }
                }

                // Redirigir a la página de Checkout si todo está bien
                Response.Redirect("Checkout.aspx");
            }
            else
            {
                lblMensajeError.Text = "El carrito está vacío. Agrega productos antes de continuar.";
                lblMensajeError.Visible = true;
            }
        }

        protected void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            // Vaciar el carrito eliminando los datos de la sesión
            Session["compras"] = new CarritoCompras();

            // Actualizar el GridView y el total general
            BindGridView();

            lblTotalGeneral.Text = "Total: $0.00";
            lblMensajeError.Visible = false; // Ocultar cualquier mensaje de error

            // Opcional: Mostrar un mensaje de confirmación
            lblMensajeError.Text = "El carrito se ha limpiado correctamente.";
            lblMensajeError.CssClass = "alert alert-success";
            lblMensajeError.Visible = true;
        }
    }
}