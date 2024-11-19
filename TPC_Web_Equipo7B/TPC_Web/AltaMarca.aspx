<%@ Page Title="Alta de Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaMarca.aspx.cs" Inherits="TPC_Web.AltaMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Alta de Marcas</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="Nombre de la Marca:"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Marca" OnClick="btnGuardar_Click" CssClass="btn-primary" />
    <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar Marcas" OnClick="btnVolver_Click" CssClass="btn-secondary" />
</asp:Content>
