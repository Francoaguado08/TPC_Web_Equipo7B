using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class HistorialPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    PedidoNegocio negocio = new PedidoNegocio();

                    // Obtener los pedidos del usuario
                    List<Pedido> pedidos = negocio.ObtenerPedidosPorUsuario(usuario.IDUsuario);

                    // Configurar GridView
                    gvPedidos.DataSource = pedidos;
                    gvPedidos.AllowPaging = true;
                    gvPedidos.PageSize = 10;
                    gvPedidos.PageIndexChanging += GvPedidos_PageIndexChanging;
                    gvPedidos.DataBind();
                }
                else
                {
                    // Si no está logueado, redirige a la página de inicio
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void GvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedidos.PageIndex = e.NewPageIndex;

            // Volver a obtener los datos y enlazar al GridView
            Usuario usuario = (Usuario)Session["usuario"];
            PedidoNegocio negocio = new PedidoNegocio();
            List<Pedido> pedidos = negocio.ObtenerPedidosPorUsuario(usuario.IDUsuario);

            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        // Maneja el comando de la GridView para redirigir a la página de detalles del pedido
        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                // Obtén el ID del pedido del CommandArgument
                int index = Convert.ToInt32(e.CommandArgument);

                // Redirige a la página de detalles de pedidos, pasando el ID del pedido como parámetro
                Response.Redirect("DetallePedido.aspx?id=" + index);
            }
        }
    }
}
