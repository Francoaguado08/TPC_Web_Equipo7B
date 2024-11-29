<%@ Page Title="Detalle de Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="TPC_Web.DetallePedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Detalles del Pedido</h2>

    <!-- GridView que mostrará los detalles del pedido -->
    <asp:GridView ID="gvDetallePedido" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="IDDetallePedido" HeaderText="IDDetallePedido" SortExpression="IDDetallePedido" />
            <asp:BoundField DataField="IDPedido" HeaderText="IDPedido" SortExpression="IDPedido" />
            <asp:BoundField DataField="IDArticulo" HeaderText="IDArticulo" SortExpression="IDArticulo" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecioUnitario" />
        </Columns>
    </asp:GridView>
</asp:Content>
