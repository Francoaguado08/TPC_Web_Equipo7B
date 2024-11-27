using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace TPC_Web
{
    public partial class ConfirmacionCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar si hay un carrito en la sesión y datos del usuario o checkout
            if (Session["compras"] == null || (Session["usuario"] == null && Session["datosCheckout"] == null))
            {
                // Redirigir al carrito si no hay datos válidos
                Response.Redirect("Compras.aspx");
                return;
            }



            if (!IsPostBack)
            {
                // Cargar resumen del pedido desde el carrito en la sesión
                CarritoCompras carrito = (CarritoCompras)Session["compras"];
                if (carrito != null)
                {
                    // Mostrar productos en el GridView
                    var resumen = carrito.ObtenerProductos().Select(p => new
                    {
                        p.Nombre,
                        p.Cantidad,
                        Precio = p.Precio,
                        Subtotal = p.Cantidad * p.Precio
                    }).ToList();

                    gvResumenPedido.DataSource = resumen;
                    gvResumenPedido.DataBind();

                    // Calcular y mostrar el total
                    lblTotal.Text = "Total: " + carrito.ObtenerTotal().ToString("C");
                }

                // Verificar si el usuario está registrado
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario != null)
                {
                    // Usuario registrado: Cargar datos personales desde la base de datos
                    DatosPersonales datos = ObtenerDatosPersonales(usuario.IDUsuario);
                    if (datos != null)
                    {
                        lblNombre.Text = $"{datos.Nombre} {datos.Apellido}";
                        lblDomicilio.Text = datos.Domicilio;
                        lblTelefono.Text = datos.Telefono;
                        lblDNI.Text = datos.DNI;
                        lblPais.Text = datos.Pais;  
                    }
                }
                else
                {
                    // Usuario no registrado: Cargar datos del formulario de checkout
                    DatosPersonales datosCheckout = (DatosPersonales)Session["datosCheckout"];

                    if (datosCheckout != null)
                    {
                        lblNombre.Text = $"{datosCheckout.Nombre} {datosCheckout.Apellido}";
                        lblDomicilio.Text = datosCheckout.Domicilio;
                        lblTelefono.Text = datosCheckout.Telefono;
                        lblPais.Text = datosCheckout.Pais;
                        lblDNI.Text = datosCheckout.DNI;    

                    }
                }
            }

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

















        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {

            // Validaciones iniciales
            CarritoCompras carrito = (CarritoCompras)Session["compras"];
            Usuario usuario = (Usuario)Session["usuario"];

           
            
            DatosPersonales datos = usuario != null
                ? ObtenerDatosPersonales(usuario.IDUsuario) //USUARIO REGISTRADO
                : (DatosPersonales)Session["datosCheckout"];//USUARIO SIN REGISTRARSE

            if (carrito == null || carrito.ObtenerProductos().Count == 0 || datos == null)
            {
                lblError.Text = "No se pudo procesar tu pedido. Verifica tu carrito o datos.";
                lblError.Visible = true;
                return;
            }



            try
            {
                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                bool resultado = pedidoNegocio.RegistrarPedidoCompleto(usuario?.IDUsuario, carrito, datos);

                if (resultado)
                {
                    // Limpiar la sesión y redirigir
                    Session["compras"] = null;
                    Session["datosCheckout"] = null;
                    Response.Redirect("Gracias.aspx");
                }
                else
                {
                    lblError.Text = "No se pudo registrar tu pedido. Por favor, intenta nuevamente.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error. Por favor, intenta nuevamente.";
                lblError.Visible = true;
            }





        }
 
    
    
    
    
    
    
    }
}