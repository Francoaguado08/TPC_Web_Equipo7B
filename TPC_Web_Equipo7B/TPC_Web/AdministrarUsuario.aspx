<%@ Page Title="Administrar Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuario.aspx.cs" Inherits="TPC_Web.AdministrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-top: 50px;">
        <h1>Administrar Usuarios</h1>
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
            EmptyDataText="No se encontraron usuarios"
            CssClass="table table-bordered"
            OnRowCommand="gvUsuarios_RowCommand"
            OnRowEditing="gvUsuarios_RowEditing"
            OnRowUpdating="gvUsuarios_RowUpdating"
            OnRowCancelingEdit="gvUsuarios_RowCancelingEdit"
            DataKeyNames="IDUsuario">
            <Columns>
                <asp:BoundField DataField="IDUsuario" HeaderText="ID" SortExpression="IDUsuario" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ReadOnly="True" />
                <asp:TemplateField HeaderText="Rol">
                    <ItemTemplate>
                        <asp:Label ID="lblRol" runat="server" Text='<%# Bind("tipousuario") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRol" runat="server">
                            <asp:ListItem Value="1" Text="Administrador"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Usuario"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEliminar" runat="server" CommandName="EliminarUsuario"
                            CommandArgument='<%# Container.DataItemIndex %>'
                            Text="Eliminar" CssClass="btn btn-danger btn-sm"
                            OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este usuario?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
    </div>


</asp:Content>
