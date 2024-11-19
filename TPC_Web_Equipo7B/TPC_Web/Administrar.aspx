<%@ Page Title="Administrar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="TPC_Web.Administrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-top: 50px;">
        <h1>Administrar Secciones</h1>
        <asp:Button ID="btnAdministrarArticulos" runat="server" Text="Administrar Artículos" OnClick="btnAdministrarArticulos_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnAdministrarCategoria" runat="server" Text="Administrar Categorías" OnClick="btnAdministrarCategoria_Click" CssClass="btn btn-success" />
        <asp:Button ID="btnAdministrarMarca" runat="server" Text="Administrar Marcas" OnClick="btnAdministrarMarca_Click" CssClass="btn btn-info" />
    </div>
</asp:Content>
