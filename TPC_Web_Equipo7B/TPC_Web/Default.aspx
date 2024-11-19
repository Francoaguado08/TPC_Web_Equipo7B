<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="text-center mb-4">
            <img src="banner.png" alt="Banner" class="img-fluid mt-3">
        </div>

        <!-- FILTROS -->
        <div class="bg-dark text-white p-3 mb-4 d-flex justify-content-center align-items-center">
            <asp:Label ID="lblFiltrarPor" runat="server" Text="Filtrar por:" CssClass="me-3" />
            <asp:DropDownList ID="ddlFiltrarPor" runat="server" CssClass="form-select me-3" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltrarPor_SelectedIndexChanged">

            </asp:DropDownList>

            <asp:Label ID="lblCriterio" runat="server" Text="Criterio:" CssClass="me-3" />
            <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select me-3" AutoPostBack="true" OnSelectedIndexChanged="ddlCriterio_SelectedIndexChanged" />
            
            <asp:Button ID="btnAplicarFiltro" runat="server" Text="Aplicar filtro" CssClass="btn btn-primary me-3" OnClick="btnAplicarFiltro_Click" />
            <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar filtros" CssClass="btn btn-secondary me-3" OnClick="btnLimpiarFiltro_Click" />

            <div class="input-group ms-3">
                <asp:TextBox ID="tbxBuscar" CssClass="form-control" runat="server" />
                <asp:Button Text="Buscar" ID="btnBuscar" CssClass="btn btn-secondary" runat="server" OnClick="btnBuscar_Click1" />
            </div>
        </div>

        <!-- TARJETAS -->
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <% foreach (Dominio.Articulo arti in listaArticulo) { %>
            <div class="col">
                <div class="card h-100">
                    <%
                        string defaultUrl = "https://nayemdevs.com/wp-content/uploads/2020/03/default-product-image.png";
                        string imagenIndex = defaultUrl;

                        if (arti.ImagenURL != null && arti.ImagenURL.Any())
                        {
                            imagenIndex = arti.ImagenURL[0];
                        }
                    %>
                    <img src="<%= imagenIndex %>" class="card-img-top" alt="Imagen del Articulo <%= arti.Nombre %>" onerror="this.src='<%= defaultUrl %>'">
                    <div class="card-body">
                        <h5 class="card-title"><%= arti.Nombre %></h5>
                        <p class="card-text"><%= arti.Descripcion %></p>
                        <p class="card-text"><strong>ARS <%= arti.Precio %></strong></p>
                        <a href="DetalleArticulo.aspx?id=<%= arti.ID %>" class="btn btn-primary">Ver más</a>
                    </div> 
                </div>
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
