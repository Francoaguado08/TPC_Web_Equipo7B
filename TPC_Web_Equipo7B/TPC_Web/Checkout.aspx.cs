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
    //public partial class Checkout : System.Web.UI.Page
    //{
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        if (!IsPostBack)
    //        {
    //            // Cargar el resumen de la compra al cargar la página por primera vez
    //            CargarResumenCompra();
    //        }
    //    }
    //    private void CargarResumenCompra()
    //    {
    //        // Obtener el carrito de la sesión
    //        CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

    //        if (miCarrito != null)
    //        {
    //            // Enlazar el carrito con el GridView
    //            dgvResumenCompra.DataSource = miCarrito.ObtenerProductos();
    //            dgvResumenCompra.DataBind();

    //            // Calcular y mostrar el total general
    //            decimal totalGeneral = miCarrito.ObtenerProductos().Sum(a => a.Precio * a.Cantidad);
    //            lblTotalGeneral.Text = totalGeneral.ToString("C");
    //        }
    //        else
    //        {
    //            lblTotalGeneral.Text = "No hay productos en el carrito.";
    //        }
    //    }



    //    protected void btnConfirmarCompra_Click(object sender, EventArgs e)
    //    {
    //        Response.Redirect("ConfirmacionCompra.aspx");
    //    }



    //}


    public partial class Checkout : Page
    {
        // Evento que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica que no sea una carga posterior (IsPostBack),
            // para evitar que se recargue innecesariamente la información
            if (!IsPostBack)
            {
                // Recupera el carrito de compras de la sesión
                CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

                // Si el carrito está vacío o no existe, redirige al menú principal
                if (miCarrito == null || !miCarrito.ObtenerProductos().Any())
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                // Asigna los productos del carrito al GridView para mostrarlos
                gvCarrito.DataSource = miCarrito.ObtenerProductos();
                gvCarrito.DataBind();

                // Calcula y muestra el total del carrito
                lblTotal.Text = "Total: " + miCarrito.ObtenerTotal().ToString("C");
            }
        }

        // Evento que se ejecuta al hacer clic en el botón Confirmar
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recupera el carrito de compras desde la sesión
                CarritoCompras carrito = (CarritoCompras)Session["compras"];

                // Inicializa el ID del usuario autenticado (si lo hay)
                int? idUsuario = null;
                if (Session["usuario"] != null)
                {
                    idUsuario = ((Usuario)Session["usuario"]).IDUsuario;
                }

                // Crea un objeto DatosPersonales con la información ingresada en el formulario
                DatosPersonales datosPersonales = new DatosPersonales
                {
                    DNI = txtDNI.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Domicilio = txtDomicilio.Text.Trim(),
                    Pais = txtPais.Text.Trim(),
                    Provincia = txtProvincia.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    IDUsuario = idUsuario ?? 0 // Si no hay usuario autenticado, usa 0
                };

                // Guarda los datos personales en la sesión para uso posterior
                Session["datosCheckout"] = datosPersonales;

                // Valida que todos los campos obligatorios estén completos
                if (string.IsNullOrWhiteSpace(datosPersonales.Nombre) ||
                    string.IsNullOrWhiteSpace(datosPersonales.Apellido) ||
                    string.IsNullOrWhiteSpace(datosPersonales.Domicilio) ||
                    string.IsNullOrWhiteSpace(datosPersonales.Pais) ||
                    string.IsNullOrWhiteSpace(datosPersonales.Provincia) ||
                    string.IsNullOrWhiteSpace(datosPersonales.Telefono))
                {
                    lblMensajeError.Text = "Completa todos los campos antes de confirmar.";
                    return;
                }

                // Registra el pedido utilizando una clase de negocio
                PedidoNegocio negocio = new PedidoNegocio();
                bool exito = negocio.RegistrarPedido(idUsuario, carrito, datosPersonales);

                // Si el registro fue exitoso, redirige a la página de confirmación
                if (exito)
                {
                    Response.Redirect("ConfirmacionCompra.aspx");
                }
                else
                {
                    // Muestra un mensaje de error si ocurre un problema al registrar el pedido
                    lblMensajeError.Text = "Ocurrió un error al procesar tu pedido. Inténtalo nuevamente.";
                }
            }
            catch (Exception ex)
            {
                // Captura y muestra cualquier error inesperado
                lblMensajeError.Text = "Ocurrió un error: " + ex.Message;
            }
        }














    }
    

}