﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialPedidos.aspx.cs" Inherits="TPC_Web.HistorialPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Estilos CSS -->
    <style>
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
            margin-top: 20px;
        }

        .grid-view th {
            background-color: #4CAF50;
            color: white;
            padding: 10px;
            text-align: left;
        }

        .grid-view td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .grid-view tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .grid-view tr:hover {
            background-color: #ddd;
        }

        .link-button {
            text-decoration: none;
            color: #007BFF;
            cursor: pointer;
        }

        .link-button:hover {
            text-decoration: underline;
        }

        h2 {
            font-family: Arial, sans-serif;
            color: #333;
            margin-bottom: 10px;
        }

        p {
            font-family: Arial, sans-serif;
            color: #666;
        }
    </style>


    <h2>Historial de Pedidos</h2>
    <p>Aquí puedes consultar los pedidos que has realizado.</p>

    <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="gvPedidos_RowCommand">
        <Columns>
            <asp:BoundField DataField="FechaPedido" HeaderText="Fecha de Pedido" SortExpression="FechaPedido" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
            

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="btnVerDetalles" runat="server" Text="<i class='fa fa-eye'></i> Ver Detalles"
                                   CommandName="VerDetalles" CommandArgument='<%# Eval("ID") %>' CssClass="link-button" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Incluir FontAwesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
</asp:Content>
