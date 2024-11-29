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

    <section cssClass="border-dark">
        <h3>Datos relevantes para finalizar la compra</h3>
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

        <h4>Email para recibir notificaciones del pedido!</h4>
        <div class="form-group">
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control" />
        </div>

        <!-- Método de pago -->
        <div class="form-group">
            <label for="ddlMetodoPago">Método de Pago:</label>
            <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged">
                <asp:ListItem Value="" Text="Seleccionar método de pago" />
                <asp:ListItem Value="Transferencia" Text="Transferencia Bancaria" />
                <asp:ListItem Value="TarjetaCredito" Text="Pagar con Tarjeta de Crédito" />
            </asp:DropDownList>
        </div>

        <!-- Información para transferencia bancaria -->
        <div id="divTransferencia" runat="server" visible="false" class="form-group">
            <h4>Detalles para realizar la transferencia:</h4>
            <p><strong>Banco:</strong> Banco BBVA</p>
            <p><strong>Titular:</strong> E7B SRL. </p>
            <p><strong>CUIT:</strong> 30-70345234-7</p>
            <p><strong>Cuenta:</strong> 347-456789/2</p>
            <p><strong>CBU:</strong> 0987654321234567891234</p>
            <p><strong>Total a transferir:</strong> <asp:Label ID="lblTotalTransferencia" runat="server" CssClass="total-label"></asp:Label></p>
        </div>

        <!-- Formulario para tarjeta de crédito -->
        <div id="divTarjeta" runat="server" visible="false" class="form-group">
            <h4>Ingrese los datos de su tarjeta:</h4>
            <asp:TextBox ID="txtNumeroTarjeta" runat="server" Placeholder="Número de tarjeta" CssClass="form-control" MaxLength="16" />
            <asp:TextBox ID="txtNombreTitular" runat="server" Placeholder="Nombre del titular" CssClass="form-control" />
            <asp:TextBox ID="txtFechaVencimiento" runat="server" Placeholder="Fecha de vencimiento (MM/AA)" CssClass="form-control" />
            <asp:TextBox ID="txtCodigoSeguridad" runat="server" Placeholder="Código de seguridad (CVV)" CssClass="form-control" MaxLength="3" />
        </div>



        <div class="form-actions">
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-primary" OnClick="btnConfirmar_Click" />
        </div>
        <asp:Label ID="lblMensajeError" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>
    </section>
</asp:Content>
