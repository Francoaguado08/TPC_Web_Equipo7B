<%@ Page Title="Administrar Categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarCategorias.aspx.cs" Inherits="TPC_Web.AdministrarCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Administrar Categorías</h2>

    <%-- Label para mostrar errores --%>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

    <asp:GridView ID="gvCategorias" DataKeyNames="ID" runat="server" CssClass="table table-dark table-bordered" 
        AutoGenerateColumns="false" 
        OnRowEditing="gvCategorias_RowEditing" 
        OnRowUpdating="gvCategorias_RowUpdating" 
        OnRowDeleting="gvCategorias_RowDeleting" 
        OnRowCancelingEdit="gvCategorias_RowCancelingEdit">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" ReadOnly="true" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

            <%-- Botones personalizados --%>
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <div style="display: flex; gap: 5px; justify-content: center;">
                        <%-- Botón Editar --%>
                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning">
                            Editar
                        </asp:LinkButton>

                        <%-- Botón Eliminar --%>
                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Estás seguro de eliminar esta categoría?');">
                            Eliminar
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div style="display: flex; gap: 5px; justify-content: center;">
                        <%-- Botón Guardar --%>
                        <asp:LinkButton ID="btnGuardar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success">
                            Guardar
                        </asp:LinkButton>

                        <%-- Botón Cancelar --%>
                        <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary">
                            Cancelar
                        </asp:LinkButton>
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />

    <%-- Botones adicionales --%>
    <div style="display: flex; gap: 10px; justify-content: center; margin-top: 20px;">
        <%-- Botón para agregar nueva categoría --%>
        <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría" OnClick="btnNuevaCategoria_Click" CssClass="btn btn-primary" />

        <%-- Botón para volver a Administrar.aspx --%>
        <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
    </div>
</asp:Content>
