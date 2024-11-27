<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionCompra.aspx.cs" Inherits="TPC_Web.ConfirmacionCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container mt-5">
        <!-- Encabezado -->
        <div class="text-center">
            <h2 class="text-success">¡Gracias por tu compra!</h2>
            <p class="lead">Tu pedido ha sido registrado con éxito.</p>
        </div>

        <!-- Resumen del pedido -->
        <div class="card shadow-sm mt-4">
            <div class="card-header bg-dark text-white">
                <h3 class="mb-0">Resumen del pedido</h3>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvResumenPedido" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
                <div class="text-end mt-3">
                    <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold fs-4 text-primary"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Datos del cliente -->
        <div class="card shadow-sm mt-4">
            <div class="card-header bg-dark text-white">
                <h3 class="mb-0">Datos del Cliente</h3>
            </div>
            <div class="card-body">
                <p><strong>Nombre:</strong>
                    <asp:Label ID="lblNombre" runat="server" CssClass="text-muted"></asp:Label></p>
                <p><strong>Domicilio:</strong>
                    <asp:Label ID="lblDomicilio" runat="server" CssClass="text-muted"></asp:Label></p>
                <p><strong>Teléfono:</strong>
                    <asp:Label ID="lblTelefono" runat="server" CssClass="text-muted"></asp:Label></p>
                <p><strong>DNI:</strong>
                    <asp:Label ID="lblDNI" runat="server" CssClass="text-muted"></asp:Label></p>
                <p><strong>Pais:</strong>
                    <asp:Label ID="lblPais" runat="server" CssClass="text-muted"></asp:Label></p>
                

            </div>
        </div>

        <!-- Método de pago -->
        <div class="card shadow-sm mt-4">
            <div class="card-body">
                <h3 class="text-center text-info">Método de pago</h3>
                <p class="text-center">Coordinaremos por WhatsApp al número proporcionado.</p>
            </div>
        </div>

        <!-- Botón de confirmar -->
        <div class="text-center mt-4">
            <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-success btn-lg" OnClick="btnConfirmarCompra_Click" />
        </div>

        <!-- Mensaje de error -->
        <div class="mt-3 text-center">
            <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold" Visible="false"></asp:Label>
        </div>
    </div>

</asp:Content>
