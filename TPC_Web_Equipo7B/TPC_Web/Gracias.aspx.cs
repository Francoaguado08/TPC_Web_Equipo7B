using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class Gracias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        //private void GenerarEnlaceWhatsApp()
        //{
        //    Usuario usuario = (Usuario)Session["usuario"];
        //    DatosPersonales datos = usuario != null
        //        ? ObtenerDatosPersonales(usuario.IDUsuario)
        //        : (DatosPersonales)Session["datosCheckout"];

        //    string numeroWhatsApp = "5491"; // Reemplaza con el número de WhatsApp de tu negocio.
        //    string mensaje = $"Hola, quiero saber el estado de mi pedido [IDPedido]";
        //    string enlace = $"https://wa.me/{numeroWhatsApp}?text={Uri.EscapeDataString(mensaje)}";

        //    // Asigna el enlace al control en la página
        //    hlWhatsApp.NavigateUrl = enlace;
        //}


    }
}