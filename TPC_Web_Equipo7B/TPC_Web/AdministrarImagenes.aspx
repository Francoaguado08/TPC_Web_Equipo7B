<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarImagenes.aspx.cs" Inherits="TPC_Web.AdministrarImagenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Gestion de Imagenes de este Articulo!</h1>
    <asp:Label ID="lblArticulo" runat="server" Font-Bold="true"></asp:Label>
    <hr />


    <%--El GridView tiene una propiedad llamada DataKeyNames que se puede usar para almacenar las claves primarias de cada fila (como el ID de la imagen) y 
    acceder a ellas de forma sencilla en los eventos de los botones.--%>


    <asp:GridView ID="gvImagenes" runat="server" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            
            <asp:BoundField DataField="ImagenURl" HeaderText="URL de la Imagen" SortExpression="ImagenURl" />

            
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                 
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        CommandArgument='<%# Eval("ID") %>' OnClick="btnEliminar_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" />


    


    <asp:TextBox ID="txtNuevaImagenURL" runat="server" CssClass="form-control"
        Placeholder="Ingrese la URL de la nueva imagen"></asp:TextBox>
    <asp:Button ID="btnAgregarImagen" runat="server" Text="Agregar Imagen" CssClass="btn btn-success mt-2" OnClick="btnAgregarImagen_Click" />
    <br />
     <asp:Button ID="btnHome" runat="server" Text="Pagina Principal" CssClass="btn btn-success mt-2" OnClick="btnHome_Click" />



</asp:Content>
