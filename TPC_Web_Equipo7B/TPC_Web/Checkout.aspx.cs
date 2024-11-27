using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail; //agrego para email!
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
            // Evitar recargas innecesarias de datos
            if (!IsPostBack)
            {

                // Validar si el usuario está logueado
                Usuario usuario = (Usuario)Session["usuario"];

                if (usuario != null)
                {
                    // Usuario logueado: cargar datos personales
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















        /* Flujo completo en palabras
           Obtener carrito y datos del usuario:

          Si es registrado, usa sus datos guardados.
          Si no, usa los datos temporales de la sesión.
          Registrar el pedido:

          Guarda el pedido en la base de datos.
          Registra los productos del carrito en los detalles del pedido.
          Agrega los datos personales si son necesarios.
          Mostrar éxito o error:

          Redirige al usuario a una página de "Gracias" si todo fue bien.
          Muestra un error en pantalla si algo falla.*/


      /* Usuario registrado---> Usa los datos de la sesión para llenar automáticamente los campos si están disponibles.
         Usuario no registrado    ---> Usa los datos del formulario que el visitante haya llenado y los almacena en la sesión (Session["datosCheckout"]).*/





        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el carrito desde la sesión
                CarritoCompras carrito = (CarritoCompras)Session["compras"];
                if (carrito == null || carrito.ObtenerProductos().Count == 0)
                {
                    lblMensajeError.Text = "El carrito está vacío. Por favor, agrega productos antes de confirmar.";
                    return;
                }

                // Verificar si el usuario está registrado
                Usuario usuario = (Usuario)Session["usuario"];

                // Obtener datos personales directamente desde el formulario o la sesión
                    DatosPersonales datosPersonales = usuario != null
                                                                        ? ObtenerDatosDesdeSesion(usuario) // Usuario registrado
                                                                        : ObtenerDatosDesdeFormulario();  // Usuario no registrado

                // Procesar el pedido
                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                bool exito = pedidoNegocio.RegistrarPedidoCompleto(usuario?.IDUsuario, carrito, datosPersonales);

                if (exito)
                {
                    // Limpiar sesiones y redirigir
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
       
        
        }//FIN btnConfirmar


        public DatosPersonales ObtenerDatosDesdeSesion(Usuario usuario)
        {
            // Datos temporales en caso de que no estén precargados 
            return new DatosPersonales
            {
                IDUsuario = usuario.IDUsuario,
                DNI = txtDNI.Text, // Si tienes un campo en el formulario
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
                // Manejar el error
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