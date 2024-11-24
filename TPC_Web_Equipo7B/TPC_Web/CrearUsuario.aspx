<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="TPC_Web.CrearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="max-width: 600px; margin: auto;">
        <h2 class="text-center">Registro de Usuario</h2>
        
        <!-- Validar Email -->
        <div id="emailValidationSection">
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese su email"></asp:TextBox>
                <asp:Label ID="lblEmailError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </div>
            <asp:Button ID="btnValidarEmail" runat="server" CssClass="btn btn-primary" Text="Validar Email" OnClick="btnValidarEmail_Click" />
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-secondary" Text="Volver al Login" OnClick="btnAceptar_Click" Visible="false" />

        </div>

        <!-- Formulario completo de usuario -->
        <div id="userDetailsSection" runat="server" style="display: none; margin-top: 20px;">
            <h3>Complete sus datos</h3>
            <div class="form-group">
                <label for="txtPassword">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDNI">DNI</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="Ingrese su DNI"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese su nombre"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtApellido">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingrese su apellido"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDomicilio">Domicilio</label>
                <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control" placeholder="Ingrese su domicilio"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPais">País</label>
                <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" placeholder="Ingrese su país"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtProvincia">Provincia</label>
                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="Ingrese su provincia"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtTelefono">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese su teléfono"></asp:TextBox>
            </div>
            <asp:Button ID="btnGuardarUsuario" runat="server" CssClass="btn btn-success" Text="Guardar Usuario" OnClick="btnGuardarUsuario_Click" />
        </div>
    </div>
</asp:Content>
