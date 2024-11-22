<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionCompra.aspx.cs" Inherits="TPC_Web.ConfirmacionCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>¡Gracias por tu compra!</h2>
    <p>Tu pedido ha sido registrado con éxito.</p>
    <h3>Resumen del pedido</h3>
    <asp:GridView ID="gvResumenPedido" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Producto" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <br />
    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>

    <h3>Datos del Cliente</h3>
    <h3>Nombre:</h3>
    <asp:Label ID="lblNombre" runat="server"></asp:Label>
    <h3>Domicilio : </h3>
    <asp:Label ID="lblDomicilio" runat="server"></asp:Label>

    <h3>Telefono : </h3>
    <asp:Label ID="lblTelefono" runat="server"></asp:Label>

    <h3>Método de pago: Coordinaremos por WhatsApp al número proporcionado. </h3>



    <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" OnClick="btnConfirmarCompra_Click" />

    <asp:Label
        ID="lblError"
        runat="server"
        ForeColor="Red"
        Visible="false">
    </asp:Label>


</asp:Content>
