<%@ Page Title="Verificar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerificarUsuario.aspx.cs" Inherits="TPC_Web.VerificarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="max-width: 400px; margin: auto;">
        <h2 class="text-center">Verificar Usuario</h2>
        
        <!-- Email -->
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese su email"></asp:TextBox>
        </div>
        
        <!-- Contraseña -->
        <div class="form-group">
            <label for="txtPassword">Contraseña</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
        </div>
        
        <!-- Botones -->
        <div class="text-center">
            <asp:Button ID="btnVerificar" runat="server" CssClass="btn btn-primary" Text="Verificar" OnClick="btnVerificar_Click" />
            <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="btnRegistrar_Click" Visible="false" />
        </div>

        <!-- Mensajes -->
        <div class="form-group" style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-center" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
