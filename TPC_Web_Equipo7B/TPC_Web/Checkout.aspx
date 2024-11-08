<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Página de resumen de la orden antes de pagar. Incluye opciones de envío y métodos de pago.--%>

   
    <%--EN CASO DE QUE NO ME FUNCIONE LO OTRO , GUARDO ESTO, NO TOCAR!--%>
    <%--  <asp:GridView ID="dgvResumenCompra" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Producto" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:TemplateField HeaderText="Subtotal">
                <ItemTemplate>
                    <%# Eval("Precio", "{0:C}") %> x <%# Eval("Cantidad") %> = <%# Convert.ToDecimal(Eval("Precio")) * Convert.ToInt32(Eval("Cantidad")) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Mostrar el total general -->
    <h3>Total:
        <asp:Label ID="lblTotalGeneral" runat="server" CssClass="fw-bold"></asp:Label></h3>

    <div>
        <asp:Button ID="btnConfirmarCompra" runat="server" CssClass="btn btn-primary" Text="Confirmar Compra" OnClick="btnConfirmarCompra_Click" />
    </div>--%>



    <div class="mb-3">
        <label for="ddlEnvio" class="form-label">Selecciona el método de envío:</label>
        <asp:DropDownList ID="ddlEnvio" runat="server" CssClass="form-control">
            <asp:ListItem Value="50">Envío estándar (ARS 50)</asp:ListItem>
            <asp:ListItem Value="100">Envío rápido (ARS 100)</asp:ListItem>
            <asp:ListItem Value="0">Retiro en tienda (Gratis)</asp:ListItem>
        </asp:DropDownList>
    </div>


    <div class="mb-3">
        <label for="ddlPago" class="form-label">Selecciona el método de pago:</label>
        <asp:DropDownList ID="ddlPago" runat="server" CssClass="form-control">
            <asp:ListItem Value="MercadoPago">Mercado Pago</asp:ListItem>
            <asp:ListItem Value="Transferencia">Transferencia Bancaria</asp:ListItem>
            <asp:ListItem Value="ContraEntrega">Pago Contra Entrega</asp:ListItem>
        </asp:DropDownList>
    </div>



    <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-primary" OnClick="btnConfirmarCompra_Click" />

    <!-- Total de la Compra -->
    <div class="mt-3">
        <asp:Label ID="lblTotal" runat="server" Text="Total: " CssClass="fs-4"></asp:Label>
    </div>










</asp:Content>
