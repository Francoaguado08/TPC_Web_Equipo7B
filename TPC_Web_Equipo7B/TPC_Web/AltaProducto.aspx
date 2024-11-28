<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="TPC_Web.AltaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="ddlCategorias" class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="txtMarcas" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <!-- Nuevo apartado para el Stock -->
            <div class="mb-3">
                <label for="txtStock" class="form-label">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Mensaje de error -->
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

            <div class="mb-3">
                <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-dark" OnClick="btnAceptar_Click" Text="Aceptar" />
                <a href="AdministrarArticulos.aspx" class="btn btn-secondary">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
