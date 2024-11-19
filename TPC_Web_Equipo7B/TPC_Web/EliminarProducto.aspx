<%@ Page Title="Eliminar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="TPC_Web.EliminarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Eliminar Producto</h2>
    <asp:Panel ID="pnlEliminarProducto" runat="server">
        <p>¿Estás seguro de que deseas eliminar el siguiente producto?</p>
        <h3><asp:Label ID="lblNombreProducto" runat="server" Text=""></asp:Label></h3>
        <asp:HiddenField ID="hfProductoID" runat="server" />
        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Eliminación" OnClick="btnConfirmar_Click" CssClass="btn btn-danger" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/AdministrarArticulos.aspx" CssClass="btn btn-secondary" />
    </asp:Panel>
</asp:Content>
