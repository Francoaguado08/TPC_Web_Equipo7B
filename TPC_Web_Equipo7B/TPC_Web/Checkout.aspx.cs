using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail; // Para validación de email
using Negocio;
using Microsoft.Win32;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.WebControls.WebParts;

namespace TPC_Web
{
    public partial class Checkout : Page
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
                lblTotal.Text = "Total: " + carrito.ObtenerTotal().ToString("C");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si los campos están rellenados
                if (!SonDatosCompletos())
                {
                    lblMensajeError.Text = "Por favor, completa todos los campos antes de continuar.";
                    return;
                }

                // Validar que el teléfono y el DNI sean numéricos
                if (!EsNumerico(txtTelefono.Text) || !EsNumerico(txtDNI.Text))
                {
                    lblMensajeError.Text = "El campo Teléfono y DNI deben contener solo números.";
                    return;
                }

                // Validar el formato del email
                if (!EsEmailValido(txtEmail.Text))
                {
                    lblMensajeError.Text = "El email ingresado no es válido. Por favor, verifica e intenta nuevamente.";
                    return;
                }

                CarritoCompras carrito = (CarritoCompras)Session["compras"];
                if (carrito == null || carrito.ObtenerProductos().Count == 0)
                {
                    lblMensajeError.Text = "El carrito está vacío. Por favor, agrega productos antes de confirmar.";
                    return;
                }

                Usuario usuario = (Usuario)Session["usuario"];

                DatosPersonales datosPersonales = usuario != null
                    ? ObtenerDatosDesdeSesion(usuario)
                    : ObtenerDatosDesdeFormulario();

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                bool exito = pedidoNegocio.RegistrarPedidoCompleto(usuario?.IDUsuario, carrito, datosPersonales);

                if (exito)
                {
                    Session["compras"] = null;
                    Session["datosCheckout"] = null;
                    Response.Redirect("Gracias.aspx");
                }
                else
                {
                    lblMensajeError.Text = "Ocurrió un problema al procesar el pedido.";
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error: " + ex.Message;
            }
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
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

        private bool EsNumerico(string texto)
        {
            return int.TryParse(texto, out _);
        }

        public DatosPersonales ObtenerDatosDesdeSesion(Usuario usuario)
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

        public DatosPersonales ObtenerDatosDesdeFormulario()
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

        public DatosPersonales ObtenerDatosPersonales(int idUsuario)
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

                datosAcceso.Lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }

            return datos;
        }
    }
}
