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
            // Verifica si es la primera carga de la página
            if (!IsPostBack)
            {
                // Obtener el carrito de la sesión o inicializar uno nuevo si no existe
                CarritoCompras miCarrito = (CarritoCompras)Session["compras"] ?? new CarritoCompras();
                Session["compras"] = miCarrito;  // Guardar el carrito en la sesión

                // Comprobar si la URL tiene un parámetro de "id" para agregar un artículo al carrito
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]); // Convertir el parámetro "id" en un número entero
                    List<Articulo> articulos = (List<Articulo>)Session["articulos"]; // Obtener la lista de artículos de la sesión

                    if (articulos != null)
                    {
                        // Buscar el artículo en la lista con el ID proporcionado
                        Articulo artAgregado = articulos.Find(x => x.ID == id);

                        if (artAgregado != null)
                        {
                            // Si se encuentra, agregar el artículo al carrito
                            miCarrito.AgregarProducto(artAgregado);
                        }
                    }
                }

                // Enlazar el carrito con el GridView para mostrar los productos agregados
                dgvCompras.DataSource = miCarrito.ObtenerProductos();
                dgvCompras.DataBind();

                // Calcular y mostrar el total general de la compra
                ActualizarTotalGeneral(miCarrito);
            }
        }

        protected void dgvCompras_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Establece el índice de la fila que se está editando
            dgvCompras.EditIndex = e.NewEditIndex;
            BindGridView();  // Volver a enlazar el GridView con los datos
        }

        protected void dgvCompras_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtener el carrito de la sesión
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

            // Obtener la fila que se está editando actualmente
            GridViewRow row = dgvCompras.Rows[e.RowIndex];
            int id = Convert.ToInt32(dgvCompras.DataKeys[e.RowIndex].Value); // Obtener el ID del producto editado

            // Obtener el nuevo valor de cantidad del TextBox
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            int nuevaCantidad;

            // Validar que la cantidad ingresada sea un número entre 1 y 10
            if (int.TryParse(txtCantidad.Text, out nuevaCantidad) && nuevaCantidad >= 1 && nuevaCantidad <= 10)
            {
                // Buscar el producto en el carrito y actualizar su cantidad
                Articulo producto = miCarrito.ObtenerProductos().Find(p => p.ID == id);

                if (producto != null)
                {
                    producto.Cantidad = nuevaCantidad; // Actualizar la cantidad del producto
                }

                // Guardar el carrito actualizado en la sesión
                Session["compras"] = miCarrito;

                // Salir del modo de edición y actualizar el GridView
                dgvCompras.EditIndex = -1;
                BindGridView();

                // Actualizar el total general con la nueva cantidad
                ActualizarTotalGeneral(miCarrito);
            }
            
        }

        protected void dgvCompras_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancelar la edición y volver al modo de visualización normal
            dgvCompras.EditIndex = -1;
            BindGridView();
        }

        private void BindGridView()
        {
            // Enlazar los datos del carrito con el GridView
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];
            dgvCompras.DataSource = miCarrito.ObtenerProductos();
            dgvCompras.DataBind();
        }

        private void ActualizarTotalGeneral(CarritoCompras miCarrito)
        {
            // Calcular el total general sumando el precio * cantidad de cada producto
            decimal totalGeneral = miCarrito.ObtenerProductos().Sum(a => a.Precio * a.Cantidad);

            // Mostrar el total general en el Label
            lblTotalGeneral.Text = "Total: " + totalGeneral.ToString("C");
        }

        protected void btnCkeckout_Click(object sender, EventArgs e)
        {
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

            if (miCarrito != null && miCarrito.ObtenerProductos().Count > 0)
            {
                // Redirigir a la página de confirmación
                Response.Redirect("Checkout.aspx");
            }
            else
            {
                // Mostrar el mensaje de error
                lblMensajeError.Text = "El carrito está vacío. Agrega productos antes de continuar.";
                lblMensajeError.Visible = true; // Asegurarse de que el mensaje sea visible
            }
        }

    }
}