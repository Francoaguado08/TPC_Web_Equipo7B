<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalesDatos.aspx.cs" Inherits="TPC_Web.PersonalesDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <asp:TextBox ID="txtDNI" runat="server" />
    <asp:TextBox ID="txtNombre" runat="server" />
    <asp:TextBox ID="txtApellido" runat="server" />
    <asp:TextBox ID="txtDomicilio" runat="server" />
     <asp:TextBox ID="txtTelefono" runat="server" />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />









</asp:Content>
