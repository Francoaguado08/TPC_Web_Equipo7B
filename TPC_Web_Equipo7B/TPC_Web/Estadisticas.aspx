<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="TPC_Web.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white text-center">
                <h3 class="mb-0">Estadísticas del Sitio</h3>
            </div>
            <div class="card-body">
                <div class="row text-center">
                    <div class="col-md-6 mb-4">
                        <div class="card border-success shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title text-success">Usuarios Registrados</h5>
                                <p class="display-4 text-success">
                                    <asp:Label ID="lblUsuariosRegistrados" runat="server" Text="0"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 mb-4">
                        <div class="card border-info shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title text-info">Compras en 2024</h5>
                                <p class="display-4 text-info">
                                    <asp:Label ID="lblCompras2024" runat="server" Text="0"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer text-center">
                <a href="Administrar.aspx" class="btn btn-primary">Volver al Panel de Administración</a>
            </div>
        </div>
    </div>










</asp:Content>
