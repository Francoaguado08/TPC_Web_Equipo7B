<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Web.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    
    <div>
        <h1 class="bottom-100">Compras</h1>
    </div>
    <h2>Detalle de compra</h2>
    <h6>Puede modificar la cantidad de compra en determinado Articulo!</h6>

    <%-- GridView para mostrar los productos en el carrito --%>
    <asp:GridView ID="dgvCompras" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnRowEditing="dgvCompras_RowEditing" OnRowUpdating="dgvCompras_RowUpdating" OnRowCancelingEdit="dgvCompras_RowCancelingEdit"
        DataKeyNames="ID">
            
        <Columns>
           
            
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" />
            <asp:BoundField DataField="Codigo" HeaderText="Código" ReadOnly="True" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" ReadOnly="True" />

          <%--  Columna editable para la Cantidad con validadores --%>
            <asp:TemplateField HeaderText="Cantidad">
                <EditItemTemplate>
                    <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
                    <asp:RangeValidator ID="rvCantidad" runat="server" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="10" Type="Integer"
                        ErrorMessage="Cantidad entre 1 y 10" Display="Dynamic" ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revCantidad" runat="server" ControlToValidate="txtCantidad"
                        ValidationExpression="^\d+$" ErrorMessage="Solo números" Display="Dynamic" ForeColor="Red" />
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

            <%--Botón para editar la cantidad --%>
            <asp:CommandField ShowEditButton="True" />


      </Columns>
    
    
    </asp:GridView>

    <%-- Mostrar el total general de la compra --%>
    <div>
        <asp:Label ID="lblTotalGeneral" runat="server" Text="Total: $0.00" CssClass="total-label" />
    </div>

  <%--   Botones para acciones adicionales --%>
    <div class="container">
        <div class="row">
            <div class="col">
                <a href="Checkout.aspx" class="btn btn-primary float-end">Comprar</a>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col">
                <a href="Default.aspx" class="btn btn-primary float-end">Volver</a>
            </div>
        </div>
    </div>




</asp:Content>
