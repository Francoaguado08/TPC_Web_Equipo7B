using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail; //agrego para email!

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalcularTotal(); // Calcular el total inicial al cargar la página
            }
        }

        protected void ddlEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularTotal(); // Actualizar el total cuando cambia el método de envío
        }

        private void CalcularTotal()
        {
            // Obtener el carrito de la sesión
            CarritoCompras miCarrito = (CarritoCompras)Session["compras"];

            // Verificar que el carrito no sea nulo
            if (miCarrito != null)
            {
                decimal totalProductos = miCarrito.ObtenerTotal(); // Total de productos en el carrito
                decimal costoEnvio = decimal.Parse(ddlEnvio.SelectedValue); // Obtener el costo de envío seleccionado
                decimal totalFinal = totalProductos + costoEnvio; // Total final

                // Actualizar el Label con el total
                lblTotal.Text = "Total: ARS " + totalFinal.ToString("N2");
            }
            else
            {
                // Manejo en caso de que el carrito no exista
                lblTotal.Text = "Total: ARS 0.00";
            }
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            string metodoPago = ddlPago.SelectedValue;

            if (metodoPago == "MercadoPago")
            {
                // Simulación de pago mediante email a través de Mercado Pago
                string emailVendedor = "@gmail.com";
                string asunto = "Pedido de Compra";
                string cuerpo = $"Solicito realizar el pago del total de: {lblTotal.Text} a través de Mercado Pago. " +
                                $"Mi método de envío es: {ddlEnvio.SelectedItem.Text}";

                EnviarEmail(emailVendedor, asunto, cuerpo);
                Response.Redirect("ConfirmacionCompra.aspx");
            }
            else
            {
                // Redirigir a Confirmación de Compra para otros métodos de pago
                Response.Redirect("ConfirmacionCompra.aspx");
            }
        }


        //FALTA AJUSTAR TODO...
        private void EnviarEmail(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                // Crear el mensaje de correo
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(destinatario);  // Destinatario del correo
                mail.Subject = asunto;      // Asunto del correo
                mail.Body = cuerpo;         // Cuerpo del correo
                mail.IsBodyHtml = true;     // Si el cuerpo es HTML (si quieres que soporte formato)

                // Dirección de correo desde donde se enviará
                mail.From = new System.Net.Mail.MailAddress("@gmail.com");

                // Configuración del cliente SMTP
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para usar con TLS
                smtp.Credentials = new System.Net.NetworkCredential("@gmail.com", "tu_contraseña"); // Tu correo y la contraseña de tu cuenta de Gmail
                smtp.EnableSsl = true; // Usar SSL para encriptar la conexión

                // Enviar el correo
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write("Error al enviar el correo: " + ex.Message);
            }
        }
    }





}