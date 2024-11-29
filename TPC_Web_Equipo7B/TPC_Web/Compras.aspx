<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Web.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-4">
        <h1 class="text-center mb-4">Compras</h1>
        <h2>Detalle de compra</h2>
        <p class="text-muted">Puede modificar la cantidad de compra en determinado artículo.</p>

        <%-- GridView para mostrar los productos en el carrito --%>
        <asp:GridView ID="dgvCompras" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="False"
            OnRowEditing="dgvCompras_RowEditing" OnRowUpdating="dgvCompras_RowUpdating" OnRowCancelingEdit="dgvCompras_RowCancelingEdit"
            DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" />
                <asp:BoundField DataField="Codigo" HeaderText="Código" ReadOnly="True" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" ReadOnly="True" />

                <%-- Columna editable para la Cantidad con validadores --%>
                <asp:TemplateField HeaderText="Cantidad">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Cantidad") %>' />
                        <asp:RangeValidator ID="rvCantidad" runat="server" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="10" Type="Integer"
                            ErrorMessage="Cantidad entre 1 y 10" Display="Dynamic" CssClass="text-danger" />
                        <asp:RegularExpressionValidator ID="revCantidad" runat="server" ControlToValidate="txtCantidad"
                            ValidationExpression="^\d+$" ErrorMessage="Solo números" Display="Dynamic" CssClass="text-danger" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- Columna para mostrar el total por producto (precio * cantidad) --%>
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <%# (Convert.ToDecimal(Eval("Precio")) * Convert.ToInt32(Eval("Cantidad"))).ToString("C") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- Botón para editar la cantidad --%>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>

        <%-- Mostrar el total general de la compra --%>
        <div class="text-end">
            <asp:Label ID="lblTotalGeneral" runat="server" Text="Total: $0.00" CssClass="fw-bold fs-5" />
        </div>

        <%-- Botones para acciones adicionales --%>
        <div class="d-flex justify-content-between mt-4">
            <asp:Button ID="btnCkeckout" runat="server" Text="Comprar" CssClass="btn btn-success" OnClick="btnCkeckout_Click" />
           
            <asp:Button ID="btnLimpiarCarrito" runat="server" Text="Limpiar Carrito" CssClass="btn btn-danger" Onclick="btnLimpiarCarrito_Click" />
            <a href="Default.aspx" class="btn btn-primary">Volver</a>
        </div>
        <br />
        <br />
        <br />  
        <%-- Mensaje de error --%>
        <asp:Label ID="lblMensajeError" runat="server" Text="Error: Algo salió mal." CssClass="text-danger mt-3 d-block" Visible="false"></asp:Label>
    </div>
</asp:Content>
