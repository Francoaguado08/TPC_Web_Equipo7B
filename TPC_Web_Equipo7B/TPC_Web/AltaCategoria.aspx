<%@ Page Title="Alta de Categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCategoria.aspx.cs" Inherits="TPC_Web.AltaCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Alta de Categorías</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="Nombre de la Categoría:"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Categoría" OnClick="btnGuardar_Click" CssClass="btn-primary" />
    <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar Categorías" OnClick="btnVolver_Click" CssClass="btn-secondary" />
</asp:Content>
