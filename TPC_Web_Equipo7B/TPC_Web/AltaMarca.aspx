<%@ Page Title="Alta de Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaMarca.aspx.cs" Inherits="TPC_Web.AltaMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4">Alta de Marcas</h2>

        <!-- Mensaje de validación o éxito -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-center d-block mb-3 text-danger"></asp:Label>

        <div class="card shadow-lg">
            <div class="card-body">
                <form>
                    <!-- Campo para el nombre de la marca -->
                    <div class="form-group mb-3">
                        <label for="txtNombre" class="form-label">Nombre de la Marca:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre de la marca"></asp:TextBox>
                    </div>

                    <!-- Botones -->
                    <div class="d-flex justify-content-between">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Marca" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnVolver" runat="server" Text="Volver a Administrar Marcas" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
