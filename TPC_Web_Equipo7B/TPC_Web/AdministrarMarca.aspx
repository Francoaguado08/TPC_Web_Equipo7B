<%@ Page Title="Administrar Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarMarcas.aspx.cs" Inherits="TPC_Web.AdministrarMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gvMarcas" DataKeyNames="ID" runat="server" CssClass="table table-dark table-bordered" 
        OnSelectedIndexChanged="gvMarcas_SelectedIndexChanged" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

            <%-- Botón de selección para gestionar la marca seleccionada --%>
            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Acción" />
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnNuevaMarca" runat="server" Text="Nueva Marca" OnClick="btnNuevaMarca_Click" CssClass="btn btn-primary" />
</asp:Content>