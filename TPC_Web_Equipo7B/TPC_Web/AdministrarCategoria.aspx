<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarCategorias.aspx.cs" Inherits="TPC_Web.AdministrarCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gvCategorias" DataKeyNames="ID" runat="server" CssClass="table table-dark table-bordered" 
        OnSelectedIndexChanged="gvCategorias_SelectedIndexChanged" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

            <%-- Botón de selección para gestionar la categoría seleccionada --%>
            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Acción" />
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría" OnClick="btnNuevaCategoria_Click" CssClass="btn btn-primary" />
</asp:Content>
