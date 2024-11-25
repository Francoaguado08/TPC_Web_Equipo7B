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
            if (!IsPostBack)
            {
                // Número de WhatsApp y mensaje
                string phoneNumber = "5491132320275";
                string message = "Hola, quiero saber el estado de mi pedido.";

                // Codificar el mensaje para URL
                string encodedMessage = Server.UrlEncode(message);

                // Construir el enlace para WhatsApp
                string whatsappLinkUrl = $"https://api.whatsapp.com/send/?phone={phoneNumber}&text={encodedMessage}&type=phone_number&app_absent=0";

                // Asignar el enlace al atributo href del control
                whatsappLink.Attributes["href"] = whatsappLinkUrl;
            }


        }



      


    }
}