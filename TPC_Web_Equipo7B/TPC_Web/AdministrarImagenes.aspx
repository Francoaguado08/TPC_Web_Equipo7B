<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarImagenes.aspx.cs" Inherits="TPC_Web.AdministrarImagenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Gestion de Imagenes de este Articulo!</h1>
    <asp:Label ID="lblArticulo" runat="server" Font-Bold="true"></asp:Label>
    <hr />

    
    <asp:GridView ID="gvImagenes" runat="server" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            
            <asp:BoundField DataField="ImagenURl" HeaderText="URL de la Imagen" />

           
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm"
                        CommandArgument='<%# Eval("ID") %>' OnClick="btnEditar_Click" />
                </ItemTemplate>
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm"
                        CommandArgument='<%# Eval("ID") %>' OnClick="btnEliminar_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <hr />

    
    <asp:Panel ID="pnlFormulario" runat="server" Visible="false">
        <asp:Label ID="lblFormulario" runat="server" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control" Placeholder="Ingrese la URL de la imagen"></asp:TextBox>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" OnClick="btnGuardar_Click" />
    </asp:Panel>

   
    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nueva Imagen" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />



</asp:Content>
