<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    
 <!-- Contenedor principal -->
 <div class="container mt-4">
     <!-- Área del banner -->
     <div id="bannerArea" class="mb-4">
         <div id="mainBannerCarousel" class="carousel slide" data-bs-ride="carousel">
             <!-- Indicadores -->
             <div class="carousel-indicators">
                 <button type="button" data-bs-target="#mainBannerCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                 <button type="button" data-bs-target="#mainBannerCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                 <button type="button" data-bs-target="#mainBannerCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
             </div>

             <!-- Contenido del Carrusel -->
             <div class="carousel-inner">
                 <div class="carousel-item active">
                     <img src="banner.png" class="d-block w-100 banner-img" alt="Primer Banner">
                 </div>
                 <div class="carousel-item">
                     <img src="banner2.png" class="d-block w-100 banner-img" alt="Segundo Banner">
                 </div>
                 <div class="carousel-item">
                     <img src="banner.png" class="d-block w-100 banner-img" alt="Tercer Banner">
                 </div>
             </div>

             <!-- Flechas del Carrusel Izq- Der -->
    <%--         <button class="carousel-control-prev" type="button" data-bs-target="#mainBannerCarousel" data-bs-slide="prev">
                 <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                 <span class="visually-hidden">Anterior</span>
             </button>
             <button class="carousel-control-next" type="button" data-bs-target="#mainBannerCarousel" data-bs-slide="next">
                 <span class="carousel-control-next-icon" aria-hidden="true"></span>
                 <span class="visually-hidden">Siguiente</span>
             </button>--%>
         </div>
     </div>

        <!-- FILTROS -->

<div class="bg-dark text-white p-3 mb-4 d-flex justify-content-center align-items-center">
    <asp:Label ID="lblFiltrarPor" runat="server" Text="Filtrar por:" CssClass="me-3" />
    <asp:DropDownList ID="ddlFiltrarPor" runat="server" CssClass="form-select me-3" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltrarPor_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Label ID="lblCriterio" runat="server" Text="Criterio:" CssClass="me-3" />
    <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select me-3" AutoPostBack="true" />
    
    <asp:Button ID="btnAplicarFiltro" runat="server" Text="Aplicar filtro" CssClass="btn btn-primary me-3" OnClick="btnAplicarFiltro_Click" />
    <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar filtros" CssClass="btn btn-secondary me-3" OnClick="btnLimpiarFiltro_Click" />

    <div class="input-group ms-3">
        <asp:TextBox ID="tbxBuscar" CssClass="form-control" runat="server" />
        <asp:Button Text="Buscar" ID="btnBuscar" CssClass="btn btn-secondary" runat="server" OnClick="btnBuscar_Click1" />
    </div>
</div>

<!-- TARJETAS -->
<% if (listaArticulo != null && listaArticulo.Count > 0) { %>
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
<% } else { %>
    <div class="alert alert-warning text-center">No se encontraron artículos.</div>
<% } %>

</asp:Content>
