<%@ Page Title="Datos Personales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalesDatos.aspx.cs" Inherits="TPC_Web.PersonalesDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-container {
            max-width: 500px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
            font-family: Arial, sans-serif;
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .form-group input {
            width: 100%;
            padding: 8px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }

        .form-group input:focus {
            border-color: #007bff;
            outline: none;
        }

        .btn-save {
            display: block;
            width: 100%;
            padding: 10px;
            font-size: 16px;
            color: #fff;
            background-color: #007bff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-save:hover {
            background-color: #0056b3;
        }

        .form-footer {
            text-align: center;
            margin-top: 15px;
            font-size: 14px;
        }

        .error-message {
            text-align: center;
            margin-bottom: 15px;
            font-size: 14px;
        }
    </style>

    <div class="form-container">
        <h2>Datos Personales</h2>

        <!-- Label para mensajes de error -->
        <asp:Label ID="lblError" runat="server" CssClass="error-message" ForeColor="Red" Visible="false"></asp:Label>

        <div class="form-group">
            <label for="txtDNI">DNI</label>
            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtApellido">Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtDomicilio">Domicilio</label>
            <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtTelefono">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn-save" OnClick="btnGuardar_Click" />

        <div class="form-footer">
            <p>Todos los campos son obligatorios.</p>
        </div>
    </div>
</asp:Content>
