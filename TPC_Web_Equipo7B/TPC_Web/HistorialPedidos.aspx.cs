using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class HistorialPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                PedidoNegocio negocio = new PedidoNegocio();

                // Obtener los pedidos del usuario
                List<Pedido> pedidos = negocio.ObtenerPedidosPorUsuario(usuario.IDUsuario);

                // Mostrar los pedidos en el GridView
                gvPedidos.DataSource = pedidos;
                gvPedidos.DataBind();
            }
            else
            {
                // Si no está logueado, redirige a la página de inicio
                Response.Redirect("Default.aspx");
            }
        }
    }
}