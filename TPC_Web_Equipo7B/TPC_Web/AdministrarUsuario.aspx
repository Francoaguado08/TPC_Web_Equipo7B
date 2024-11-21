<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuario.aspx.cs" Inherits="TPC_Web.AdministrarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-top: 50px;">
        <h1>Administrar Usuarios</h1>
        
       <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" 
    EmptyDataText="No se encontraron usuarios" 
    CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="IDUsuario" HeaderText="ID" SortExpression="IDUsuario" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" SortExpression="TipoUsuario" />
    </Columns>
</asp:GridView>


        <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
    </div>
</asp:Content> 