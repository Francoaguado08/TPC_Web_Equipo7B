using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si el parámetro 'id' está presente en la URL
                if (Request.QueryString["id"] != null)
                {
                    int idPedido = Convert.ToInt32(Request.QueryString["id"]);

                    // Instancia el negocio de pedidos
                    PedidoNegocio negocio = new PedidoNegocio();

                    // Obtener los detalles del pedido
                    List<DetallesPedidos> detalles = negocio.ObtenerDetallesPorPedido(idPedido);

                    // Enlazar los detalles al GridView
                    gvDetallePedido.DataSource = detalles;
                    gvDetallePedido.DataBind();
                }
                else
                {
                    // Si no hay ID en la URL, redirige a la página de historial de pedidos
                    Response.Redirect("HistorialPedidos.aspx");
                }
            }
        }
    }
}
