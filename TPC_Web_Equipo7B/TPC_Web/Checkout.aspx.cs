using System;
using System.Linq;
using System.Net.Mail;
using Dominio;
using Negocio;

namespace TPC_Web
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];

                if (usuario != null)
                {
                    DatosPersonales datosPersonales = ObtenerDatosPersonales(usuario.IDUsuario);
                    if (datosPersonales != null)
                    {
                        txtNombre.Text = datosPersonales.Nombre;
                        txtApellido.Text = datosPersonales.Apellido;
                        txtDomicilio.Text = datosPersonales.Domicilio;
                        txtTelefono.Text = datosPersonales.Telefono;
                        txtDNI.Text = datosPersonales.DNI;
                        txtPais.Text = datosPersonales.Pais;
                        txtProvincia.Text = datosPersonales.Provincia;
                        txtEmail.Text = usuario.Email;
                    }
                }

                CarritoCompras carrito = Session["compras"] as CarritoCompras;

                if (carrito == null || !carrito.ObtenerProductos().Any())
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                gvCarrito.DataSource = carrito.ObtenerProductos();
                gvCarrito.DataBind();
                lblTotal.Text = carrito.ObtenerTotal().ToString("C");
            }
        }

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodoPago = ddlMetodoPago.SelectedValue;

            // Mostrar/ocultar secciones según la selección
            divTransferencia.Visible = metodoPago == "Transferencia";
            divTarjeta.Visible = metodoPago == "TarjetaCredito";

            // Actualizar el total en el caso de transferencia
            if (metodoPago == "Transferencia")
            {
                CarritoCompras carrito = (CarritoCompras)Session["compras"];
                if (carrito != null)
                {
                    lblTotalTransferencia.Text = carrito.ObtenerTotal().ToString("C");
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SonDatosCompletos())
                {
                    lblMensajeError.Text = "Por favor, completa todos los campos antes de continuar.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(ddlMetodoPago.SelectedValue))
                {
                    lblMensajeError.Text = "Por favor, selecciona un método de pago antes de continuar.";
                    return;
                }

                string metodoPago = ddlMetodoPago.SelectedValue;

                if (metodoPago == "Transferencia")
                {
                    lblMensajeError.Text = "Tu pedido se ha registrado. Realiza la transferencia para completar tu compra.";
                    RegistrarPedidoEnBaseDeDatos();
                }
                else if (metodoPago == "TarjetaCredito")
                {
                    if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) ||
                        string.IsNullOrWhiteSpace(txtNombreTitular.Text) ||
                        string.IsNullOrWhiteSpace(txtFechaVencimiento.Text) ||
                        string.IsNullOrWhiteSpace(txtCodigoSeguridad.Text))
                    {
                        lblMensajeError.Text = "Por favor, completa todos los datos de la tarjeta antes de continuar.";
                        return;
                    }

                    if (!EsNumerico(txtNumeroTarjeta.Text) || txtNumeroTarjeta.Text.Length != 16)
                    {
                        lblMensajeError.Text = "El número de tarjeta no es válido.";
                        return;
                    }

                    if (!EsNumerico(txtCodigoSeguridad.Text) || txtCodigoSeguridad.Text.Length != 3)
                    {
                        lblMensajeError.Text = "El código de seguridad (CVV) debe contener 3 números.";
                        return;
                    }

                    lblMensajeError.Text = "Pago procesado exitosamente con tarjeta de crédito.";
                    RegistrarPedidoEnBaseDeDatos();
                }

                Response.Redirect("Gracias.aspx");
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error: " + ex.Message;
            }
        }

        private void RegistrarPedidoEnBaseDeDatos()
        {
            try
            {
                // **8. Registrar el pedido y actualizar la base de datos**
                CarritoCompras carrito = (CarritoCompras)Session["compras"];
                Usuario usuario = (Usuario)Session["usuario"];

                DatosPersonales datosPersonales = usuario != null
                    ? ObtenerDatosDesdeSesion(usuario) // Obtener datos desde sesión
                    : ObtenerDatosDesdeFormulario(); // O desde el formulario

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                bool exito = pedidoNegocio.RegistrarPedidoCompleto(usuario?.IDUsuario, carrito, datosPersonales);

                if (!exito)
                {
                    lblMensajeError.Text = "Ocurrió un problema al registrar el pedido.";
                }
                else
                {
                    // Vaciar el carrito tras confirmar el pedido
                    Session["compras"] = null;
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al registrar en la base de datos: " + ex.Message;
            }
        }









        private bool EsNumerico(string texto)
        {
            return long.TryParse(texto, out _);
        }

        private bool SonDatosCompletos()
        {
            return !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                   !string.IsNullOrWhiteSpace(txtApellido.Text) &&
                   !string.IsNullOrWhiteSpace(txtDNI.Text) &&
                   !string.IsNullOrWhiteSpace(txtDomicilio.Text) &&
                   !string.IsNullOrWhiteSpace(txtPais.Text) &&
                   !string.IsNullOrWhiteSpace(txtProvincia.Text) &&
                   !string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text);
        }

        private DatosPersonales ObtenerDatosDesdeSesion(Usuario usuario)
        {
            return new DatosPersonales
            {
                IDUsuario = usuario.IDUsuario,
                DNI = txtDNI.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Domicilio = txtDomicilio.Text,
                Pais = txtPais.Text,
                Provincia = txtProvincia.Text,
                Telefono = txtTelefono.Text
            };
        }

        private DatosPersonales ObtenerDatosDesdeFormulario()
        {
            return new DatosPersonales
            {
                DNI = txtDNI.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Domicilio = txtDomicilio.Text,
                Pais = txtPais.Text,
                Provincia = txtProvincia.Text,
                Telefono = txtTelefono.Text
            };
        }

        private DatosPersonales ObtenerDatosPersonales(int idUsuario)
        {
            AccesoDatos datosAcceso = new AccesoDatos();
            DatosPersonales datos = null;

            try
            {
                datosAcceso.setearConsulta("SELECT * FROM DatosPersonales WHERE IDUsuario = @IDUsuario");
                datosAcceso.setearParametro("@IDUsuario", idUsuario);
                datosAcceso.ejecutarLectura();

                if (datosAcceso.Lector.Read())
                {
                    datos = new DatosPersonales
                    {
                        ID = (int)datosAcceso.Lector["ID"],
                        IDUsuario = (int)datosAcceso.Lector["IDUsuario"],
                        DNI = datosAcceso.Lector["DNI"].ToString(),
                        Nombre = datosAcceso.Lector["Nombre"].ToString(),
                        Apellido = datosAcceso.Lector["Apellido"].ToString(),
                        Domicilio = datosAcceso.Lector["Domicilio"].ToString(),
                        Pais = datosAcceso.Lector["Pais"].ToString(),
                        Provincia = datosAcceso.Lector["Provincia"].ToString(),
                        Telefono = datosAcceso.Lector["Telefono"].ToString()
                    };
                }
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }

            return datos;
        }
    }
}
