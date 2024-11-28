<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarArticulos.aspx.cs" Inherits="TPC_Web.AdministrarArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Estilo general del GridView */
        .grid {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 16px;
            text-align: left;
            background-color: #000; /* Fondo negro */
            color: #fff; /* Texto blanco */
        }

        .grid th, .grid td {
            border: 1px solid #333; /* Bordes gris oscuro */
            padding: 8px;
        }

        .grid th {
            background-color: #222; /* Fondo gris oscuro para encabezados */
            font-weight: bold;
        }

        .grid tr:nth-child(even) {
            background-color: #111; /* Fondo gris aún más oscuro para filas pares */
        }

        .grid tr:hover {
            background-color: #333; /* Resaltar filas al pasar el mouse */
        }

        /* Estilo para los botones */
        .btn {
            display: inline-block;
            padding: 5px 15px;
            font-size: 14px;
            color: #fff;
            text-align: center;
            text-decoration: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-agregar {
            background-color: #28a745;
            border: none;
        }

        .btn-agregar:hover {
            background-color: #218838;
        }

        .btn-editar {
            background-color: #007bff;
        }

        .btn-editar:hover {
            background-color: #0056b3;
        }

        .btn-eliminar {
            background-color: #dc3545;
        }

        .btn-eliminar:hover {
            background-color: #c82333;
        }

        .btn-imagenes {
            background-color: #17a2b8;
        }

        .btn-imagenes:hover {
            background-color: #117a8b;
        }

        /* Estilo para el campo de texto */
        .input-cantidad {
            width: 60px;
            padding: 5px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        /* Alineación de los elementos */
        .acciones {
            display: flex;
            align-items: center;
            gap: 10px;
        }
    </style>

    <asp:GridView ID="gvArticulos" DataKeyNames="ID" runat="server" CssClass="grid" AutoGenerateColumns="false"
        OnRowCommand="gvArticulos_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:BoundField HeaderText="Stock Disponible" DataField="Stock" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Nombre" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Nombre" />

            <asp:TemplateField HeaderText="Agregar Stock">
                <ItemTemplate>
                    <div class="acciones">
                        <asp:TextBox ID="txtNuevoStock" runat="server" CssClass="input-cantidad" Placeholder="Cantidad"></asp:TextBox>
                        <asp:Button ID="btnAgregarStock" runat="server" CssClass="btn btn-agregar"
                            CommandName="AgregarStock" CommandArgument='<%# Eval("ID") %>' Text="Agregar" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <a href='<%# "EditarProducto.aspx?id=" + Eval("ID") %>' class="btn btn-editar">✏ Editar</a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <a href='<%# "EliminarProducto.aspx?id=" + Eval("ID") %>' class="btn btn-eliminar">❌ Eliminar</a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Imágenes">
                <ItemTemplate>
                    <a href='<%# "AdministrarImagenes.aspx?id=" + Eval("ID") %>' class="btn btn-imagenes">🖼️ Imágenes</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnNuevoArticulo" runat="server" Text="Nuevo Artículo" OnClick="btnNuevoArticulo_Click"
        CssClass="btn btn-agregar" />

    <br />
    <br />
    <br />

    <div>
        <asp:Button ID="btnHome" runat="server" Text="Menú Administrar" OnClick="btnHome_Click"
            CssClass="btn btn-eliminar" />
    </div>

</asp:Content>
