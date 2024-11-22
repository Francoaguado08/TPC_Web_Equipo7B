<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Checkout</h2>

    <h3>Resumen de tu compra:</h3>
    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Producto" />

            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />

            <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>

        
        <asp:Label ID="lblTotal" runat="server"></asp:Label>

    <h3>Datos Personales:</h3>
    <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombre" />
    <asp:TextBox ID="txtApellido" runat="server" Placeholder="Apellido" />
    <asp:TextBox ID="txtDNI" runat="server" Placeholder="DNI" />
    <asp:TextBox ID="txtDomicilio" runat="server" Placeholder="Domicilio" />
    <asp:TextBox ID="txtPais" runat="server" Placeholder="País" />
    <asp:TextBox ID="txtProvincia" runat="server" Placeholder="Provincia" />
    <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Teléfono" />

    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" OnClick="btnConfirmar_Click" />

    <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red"></asp:Label>




</asp:Content>
