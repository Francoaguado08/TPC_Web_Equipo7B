<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarArticulos.aspx.cs" Inherits="TPC_Web.AdministrarArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--DataKeyNames="Id": Esto indica que el GridView usará el campo Id como clave primaria para cada fila. Esto es útil porque cuando seleccionamos un artículo, podemos acceder a su Id fácilmente--%>.
    <asp:GridView ID="gvArticulos" DataKeyNames="ID"  runat="server" CssClass="table table-dark table-bordered" OnSelectedIndexChanged="gvArticulos_SelectedIndexChanged" AutoGenerateColumns="false" >
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Nombre" SortExpression="Marca.Nombre" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Nombre" SortExpression="Categoria.Nombre" />

            <%-- !-- Botón de selección que permite redirigir -->--%>
            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Accion" />



        </Columns>
    </asp:GridView>




    <br />
    <asp:Button ID="btnNuevoArticulo" runat="server" Text="Nuevo Artículo" OnClick="btnNuevoArticulo_Click" CssClass="btn btn-primary" />










</asp:Content>
