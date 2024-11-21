<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarArticulos.aspx.cs" Inherits="TPC_Web.AdministrarArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .oculto {
            display: none;
        }
    </style>


    <%--DataKeyNames="Id": Esto indica que el GridView usará el campo Id como clave primaria para cada fila. Esto es útil porque cuando seleccionamos un artículo, podemos acceder a su Id fácilmente--%>.
    <asp:GridView ID="gvArticulos" DataKeyNames="ID" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
        <Columns>

            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Nombre" SortExpression="Marca.Nombre" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Nombre" SortExpression="Categoria.Nombre" />


            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <a href='<%# "EditarProducto.aspx?id=" + Eval("ID") %>' class="btn btn-primary btn-sm">✏ Editar</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <a href='<%# "EliminarProducto.aspx?id=" + Eval("ID") %>' class="btn btn-danger btn-sm">❌ Eliminar</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Imágenes">
                <ItemTemplate>
                    <a href='<%# "AdministrarImagenes.aspx?id=" + Eval("ID") %>' class="btn btn-info btn-sm">🖼️ Imágenes</a>
                </ItemTemplate>
            </asp:TemplateField>



        </Columns>
    </asp:GridView>




    <br />
    <asp:Button ID="btnNuevoArticulo" runat="server" Text="Nuevo Artículo" OnClick="btnNuevoArticulo_Click" CssClass="btn btn-outline-success" />

    <br />
    <br />
    <br />
    
    
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Menu Principal" OnClick="btnHome_Click" CssClass="btn btn-outline-danger" />
    </div>










</asp:Content>
