<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container" style="max-width: 400px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;">
        <h2 class="text-center">Iniciar Sesión</h2>
        
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        
        
        <div class="form-group">
            <label for="txtUsername">Email</label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Ingrese su email"></asp:TextBox>
        </div>


        <div class="form-group">
            <label for="txtPassword">Contraseña</label>
            <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server" placeholder="Ingrese su contraseña"></asp:TextBox>
        </div>

        
      



        <div class="text-center" style="margin-top: 20px;">
            <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-block" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
        </div>
    </div>
</asp:Content>
