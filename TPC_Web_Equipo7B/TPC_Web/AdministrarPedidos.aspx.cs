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
            // Validar si el usuario tiene acceso (tipoUsuario == 1 significa administrador).
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            PedidoNegocio negocio = new PedidoNegocio();
            List<Pedido> pedidos = negocio.ObtenerTodosLosPedidos();

            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void GvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Cambiar al índice de página seleccionado y recargar los pedidos.
            gvPedidos.PageIndex = e.NewPageIndex;
            CargarPedidos();
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                // Obtener el ID del pedido desde el CommandArgument.
                int idPedido = Convert.ToInt32(e.CommandArgument);

                // Obtener la fila que disparó el evento.
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    string nuevoEstado = ddlEstado.SelectedValue;

                    // Modificar el estado del pedido.
                    PedidoNegocio negocio = new PedidoNegocio();
                    negocio.ModificarEstadoPedido(idPedido, nuevoEstado);

                    // Recargar los pedidos después de la actualización.
                    CargarPedidos();
                }
            }
        }

















    }
}
