<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarImagenes.aspx.cs" Inherits="TPC_Web.GestionarImagenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    div>
       
    <h2>Gestionar Imágenes del Artículo</h2>
    <asp:Label ID="lblArticulo" runat="server" Text="Artículo: "></asp:Label>
    <br />
    <br />
    
    
    <asp:GridView ID="gvImagenes" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="URL" HeaderText="URL de Imagen" />
            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <br />


    <asp:Label ID="lblNuevaImagen" runat="server" Text="Agregar nueva URL de imagen:"></asp:Label>
    <br />
    <asp:TextBox ID="txtNuevaImagen" runat="server" Width="400"></asp:TextBox>
    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
    <br />
    <br />
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click"/>








</asp:Content>
