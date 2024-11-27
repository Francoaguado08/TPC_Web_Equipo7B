<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Checkout</h2>

    <!-- Resumen de la compra -->
    <section>
        <h3>Resumen de tu compra:</h3>
        <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        <div class="total">
            <strong>Total:</strong> 
            <asp:Label ID="lblTotal" runat="server" CssClass="total-label"></asp:Label>
        </div>
    </section>

    <!-- Datos personales -->
    <section>
        <h3>DATOS DE FACTURACION:</h3>
        <div class="form-group">
            <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombre" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtApellido" runat="server" Placeholder="Apellido" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtDNI" runat="server" Placeholder="DNI" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtDomicilio" runat="server" Placeholder="Domicilio" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtPais" runat="server" Placeholder="País" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtProvincia" runat="server" Placeholder="Provincia" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Teléfono" CssClass="form-control" />
        </div>
        <div class="form-actions">
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-primary" OnClick="btnConfirmar_Click" />
        </div>
        <asp:Label ID="lblMensajeError" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>
    </section>
</asp:Content>
