using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail; //agrego para email!
using Negocio;

namespace TPC_Web
{


    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Evitar recargas innecesarias de datos
            if (!IsPostBack)
            {
                // Recuperar el carrito de compras desde la sesión
                CarritoCompras carrito = Session["compras"] as CarritoCompras;

                // Validar si el carrito está vacío o no existe
                if (carrito == null || !carrito.ObtenerProductos().Any())
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                // Mostrar los productos del carrito en el GridView
                gvCarrito.DataSource = carrito.ObtenerProductos();
                gvCarrito.DataBind();

                // Mostrar el total del carrito
                lblTotal.Text = "Total: " + carrito.ObtenerTotal().ToString("C");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recuperar el carrito de la sesión
                CarritoCompras carrito = Session["compras"] as CarritoCompras;

                if (carrito == null || !carrito.ObtenerProductos().Any())
                {
                    lblMensajeError.Text = "El carrito está vacío.";
                    return;
                }

                // Recuperar el usuario autenticado de la sesión (si existe)
                int? idUsuario = null;
                if (Session["usuario"] != null)
                {
                    idUsuario = ((Usuario)Session["usuario"]).IDUsuario;
                }

                // Crear un objeto con los datos personales
                DatosPersonales datos = new DatosPersonales
                {
                    DNI = txtDNI.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Domicilio = txtDomicilio.Text.Trim(),
                    Pais = txtPais.Text.Trim(),
                    Provincia = txtProvincia.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    IDUsuario = idUsuario ?? 0 // Si no hay usuario autenticado, usar 0
                };

                // Validar que los datos personales no estén vacíos
                if (string.IsNullOrWhiteSpace(datos.Nombre) ||
                    string.IsNullOrWhiteSpace(datos.Apellido) ||
                    string.IsNullOrWhiteSpace(datos.Domicilio) ||
                    string.IsNullOrWhiteSpace(datos.Pais) ||
                    string.IsNullOrWhiteSpace(datos.Provincia) ||
                    string.IsNullOrWhiteSpace(datos.Telefono))
                {
                    lblMensajeError.Text = "Completa todos los campos obligatorios.";
                    return;
                }

                // Guardar los datos personales en la sesión
                Session["datosCheckout"] = datos;

                // Registrar el pedido
                PedidoNegocio negocio = new PedidoNegocio();
                bool exito = negocio.RegistrarPedido(idUsuario, carrito, datos);

                if (exito)
                {
                    Response.Redirect("ConfirmacionCompra.aspx");
                }
                else
                {
                    lblMensajeError.Text = "Ocurrió un error al procesar el pedido. Inténtalo de nuevo.";
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = $"Ocurrió un error inesperado: {ex.Message}<br />{ex.StackTrace}";
                // Opcional: Registrar el error en logs o consola
                System.Diagnostics.Debug.WriteLine("Error en Checkout: " + ex.ToString());
            }
        }













    }

}