using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> pedidos = negocio.ObtenerTodosLosPedidos();

                gvPedidos.DataSource = pedidos;
                gvPedidos.AllowPaging = true;
                gvPedidos.PageSize = 10;
                gvPedidos.PageIndexChanging += GvPedidos_PageIndexChanging;
                gvPedidos.DataBind();
            }
        }

        private void GvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedidos.PageIndex = e.NewPageIndex;
            PedidoNegocio negocio = new PedidoNegocio();
            List<Pedido> pedidos = negocio.ObtenerTodosLosPedidos();
            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                // Obtener el ID del pedido desde el CommandArgument
                int idPedido = Convert.ToInt32(e.CommandArgument); // El CommandArgument tiene el ID del pedido

                // Obtener la fila que disparó el evento
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    string nuevoEstado = ddlEstado.SelectedValue;

                    // Crear una instancia de PedidoNegocio para modificar el estado
                    PedidoNegocio negocio = new PedidoNegocio();

                    // Modificar el estado del pedido
                    negocio.ModificarEstadoPedido(idPedido, nuevoEstado);

                    // Recargar los pedidos y volver a enlazarlos al GridView
                    List<Pedido> pedidos = negocio.ObtenerTodosLosPedidos();
                    gvPedidos.DataSource = pedidos;
                    gvPedidos.DataBind();
                }
            }
        }

    }
}
