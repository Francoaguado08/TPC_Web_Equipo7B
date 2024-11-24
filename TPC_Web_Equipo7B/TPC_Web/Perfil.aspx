<%@ Page Title="Perfil de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPerfil.aspx.cs" Inherits="TPC_Web.EditarPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-container {
            max-width: 600px;
            margin: 50px auto;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .form-label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
        }

        .btn {
            display: inline-block;
            padding: 10px 20px;
            font-size: 14px;
            border: none;
            border-radius: 5px;
            text-align: center;
            cursor: pointer;
            text-decoration: none;
        }

        .btn-primary {
            background-color: #007bff;
            color: #fff;
        }

        .btn-secondary {
            background-color: #6c757d;
            color: #fff;
        }

        .button-container {
            text-align: center;
        }
    </style>

    <div class="form-container">
        <asp:Panel ID="pnlEditarPerfil" runat="server">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblApellido" runat="server" Text="Apellido:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblPais" runat="server" Text="País:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <asp:Label ID="lblProvincia" runat="server" Text="Provincia:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

            <div class="button-container">
                <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click" CssClass="btn btn-primary" Visible="false" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
