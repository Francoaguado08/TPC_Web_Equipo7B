<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialPedidos.aspx.cs" Inherits="TPC_Web.HistorialPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" >
    <Columns>
        <asp:BoundField DataField="FechaPedido" HeaderText="Fecha de Pedido" SortExpression="FechaPedido" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
        <asp:ButtonField ButtonType="Link" Text="Ver Detalles" CommandName="VerDetalles" />
    </Columns>
</asp:GridView>






</asp:Content>
