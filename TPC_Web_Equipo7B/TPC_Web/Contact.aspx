<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="TPC_Web.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contact-form-container">
        <div class="form-header">DEJANOS TU COMENTARIO</div>
        <div class="form-body">
            <div class="form-group">
                <label class="form-label" for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label" for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label" for="txtMensaje">Mensaje:</label>
                <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn-submit" OnClick="btnEnviar_Click" />
            </div>
            <div class="form-message">
                <asp:Label ID="lblResultado" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
